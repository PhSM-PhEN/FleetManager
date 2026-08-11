using FleetManager.Domain.Enum;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Domain.Entities
{
    public class Contract : AuditableEntity
    {
        /// <summary>
        /// Prazo máximo de atraso (em dias) a partir do qual não é mais permitido renovar:
        /// o cliente é obrigado a devolver o veículo e quitar a multa (Complete) em vez de continuar com ele.
        /// </summary>
        private const int MaxOverdueDaysAllowedForRenewal = 3;

        public long VehicleId { get; private set; }
        public long TenantId { get; private set; }
        public long RentalPlanId { get; private set; }
        public RentalType RentalType { get; private set; }
        public long StartMileage { get; private set; }
        public long EndMileage { get; private set; }
        public long MileageContracted { get; private set; }
        public decimal SnapshotPriceDailyRate { get; private set; }
        public decimal SnapshotPriceMonthlyRate { get; private set; }
        public decimal SnapshotPricePerExtraMileage { get; private set; }
        public decimal TotalAmount { get; private set; }
        public int TotalDays { get; private set; }
        public long? FinalMileage { get; private set; }
        public decimal? ExcessMileageFee { get; private set; }
        public int DaysLate { get; private set; }
        public decimal? LateFee { get; private set; }

        private DateTime _pickupDateTime;
        private DateTime _returnDueDateTime;
        public DateTime PickupDateTime { get => _pickupDateTime; private set => _pickupDateTime = value; }
        public DateTime ReturnDueDateTime { get => _returnDueDateTime; private set => _returnDueDateTime = value; }

        public ContractStatus ContractStatus { get; private set; } = ContractStatus.Reserved;
        public DateTime? ActualReturnDateTime { get; private set; }

        public Vehicle Vehicle { get; internal set; } = default!;
        public Tenant Tenant { get; internal set; } = default!;
        public RentalPlan RentalPlan { get; internal set; } = default!;

        protected Contract() { }

        public Contract(long vehicleId, long tenantId, RentalPlan rentalPlan, RentalType rentalType, long startMileage,
                        long mileageContracted, decimal totalAmount, DateTime pickupDateTime, DateTime? returnDueDateTime)
        {
            VehicleId = vehicleId;
            TenantId = tenantId;
            StartMileage = startMileage;
            ApplyTerms(rentalPlan, rentalType, mileageContracted, totalAmount, pickupDateTime, returnDueDateTime);
        }

        public void Cancel()
        {
            if (ContractStatus != ContractStatus.Active && ContractStatus != ContractStatus.Reserved)
                throw new BusinessRuleException(ResourceErrorMessages.CONTRACT_NOT_ACTIVE);

            ContractStatus = ContractStatus.Cancelled;
            RegisterHistoryEvent("Cancelled");
        }

        public void Confirm()
        {
            if (ContractStatus != ContractStatus.Reserved)
                throw new BusinessRuleException(ResourceErrorMessages.CONTRACT_NOT_RESERVED);

            ContractStatus = ContractStatus.Active;
            RegisterHistoryEvent("Activated");
        }

        public void Update(RentalPlan rentalPlan, RentalType rentalType, long mileageContracted, decimal totalAmount,
                        DateTime pickupDateTime, DateTime? returnDueDateTime)
        {
            if (ContractStatus != ContractStatus.Reserved)
                throw new BusinessRuleException(ResourceErrorMessages.CONTRACT_NOT_EDITABLE);

            ApplyTerms(rentalPlan, rentalType, mileageContracted, totalAmount, pickupDateTime, returnDueDateTime);
        }

        public void Complete(DateTime actualReturnDateTime, long finalMileage)
        {
            if (ContractStatus != ContractStatus.Active && ContractStatus != ContractStatus.Overdue)
                throw new BusinessRuleException(ResourceErrorMessages.CONTRACT_NOT_ACTIVE);

            if (finalMileage < StartMileage)
                throw new BusinessRuleException(ResourceErrorMessages.END_MILEAGE_CANNOT_BE_LESS_THAN_START);

            var mileageDriven = finalMileage - StartMileage;
            var excessMileage = Math.Max(0, mileageDriven - MileageContracted);

            FinalMileage = finalMileage;
            ExcessMileageFee = excessMileage * SnapshotPricePerExtraMileage;

            DaysLate = CalculateDaysLate(ReturnDueDateTime, actualReturnDateTime);
            LateFee = DaysLate * SnapshotPriceDailyRate;

            ActualReturnDateTime = actualReturnDateTime;
            ContractStatus = ContractStatus.Finished;
            RegisterHistoryEvent("Completed");
        }

        public void MarkAsOverdue()
        {
            if (ContractStatus != ContractStatus.Active)
                throw new BusinessRuleException(ResourceErrorMessages.CONTRACT_NOT_ACTIVE);

            ContractStatus = ContractStatus.Overdue;
            RegisterHistoryEvent("MarkedAsOverdue");
        }

        /// <summary>
        /// Um contrato está "em atraso" quando passou da data/hora prevista de devolução
        /// (ReturnDueDateTime) e ainda não foi encerrado. Usado tanto pela rotina que marca
        /// contratos como Overdue quanto por qualquer consulta que precise saber o status real
        /// sem esperar o job rodar.
        /// </summary>
        public bool IsPastDueDate(DateTime referenceDateTime)
        {
            return (ContractStatus == ContractStatus.Active || ContractStatus == ContractStatus.Overdue)
                && referenceDateTime > ReturnDueDateTime;
        }

        /// <summary>
        /// Quantos dias de atraso existem entre a devolução prevista e a devolução real (ou "agora",
        /// para um contrato ainda em aberto). Qualquer fração de dia já iniciada conta como um dia
        /// cheio de multa (mesma regra de arredondamento usada em CalculatePeriod), e nunca é negativo.
        /// </summary>
        public static int CalculateDaysLate(DateTime returnDueDateTime, DateTime referenceDateTime)
        {
            if (referenceDateTime <= returnDueDateTime)
                return 0;

            var lateHours = (referenceDateTime - returnDueDateTime).TotalHours;
            return (int)Math.Ceiling(lateHours / 24);
        }

        public static Contract Renew(Contract previousContract, RentalPlan currentRentalPlan, long? mileageContractedOverride)
        {
            if (previousContract.ContractStatus != ContractStatus.Active && previousContract.ContractStatus != ContractStatus.Overdue)
                throw new BusinessRuleException(ResourceErrorMessages.CONTRACT_NOT_ACTIVE);

            if (previousContract.ContractStatus == ContractStatus.Overdue)
            {
                var daysLate = CalculateDaysLate(previousContract.ReturnDueDateTime, DateTime.UtcNow);
                if (daysLate > MaxOverdueDaysAllowedForRenewal)
                    throw new BusinessRuleException(ResourceErrorMessages.RENEWAL_NOT_ALLOWED_PAST_MAX_OVERDUE_DAYS);
            }

            // A nova contagem sempre começa no vencimento original (mesmo se o contrato já está
            // atrasado e a renovação só foi registrada depois disso) — o período contratado é
            // contínuo e não depende de quando o operador processou a renovação. Os dias entre o
            // vencimento e o registro da renovação não são cobrados aqui: a multa por atraso só
            // existe se o carro for de fato devolvido atrasado (Contract.Complete), nunca na renovação.
            var pickupDateTime = previousContract.ReturnDueDateTime;

            var mileageContracted = mileageContractedOverride ?? previousContract.MileageContracted;
            var returnDueDateTime = pickupDateTime.AddDays(previousContract.TotalDays);
            var startMileage = previousContract.EndMileage;

            // Só usa o preço ATUAL do plano se o plano realmente mudou; se é o mesmo plano de
            // antes, mantém o preço congelado (snapshot) da assinatura original do contrato.
            var planChanged = currentRentalPlan.Id != previousContract.RentalPlanId;

            var totalAmount = previousContract.RentalType == RentalType.Daily
                ? (planChanged ? currentRentalPlan.DailyPrice : previousContract.SnapshotPriceDailyRate) * previousContract.TotalDays
                : (planChanged ? currentRentalPlan.MonthlyPrice : previousContract.SnapshotPriceMonthlyRate);

            var renewed = new Contract(previousContract.VehicleId, previousContract.TenantId, currentRentalPlan, previousContract.RentalType,
                startMileage, mileageContracted, totalAmount, pickupDateTime, returnDueDateTime);

            renewed.Confirm();
            previousContract.MarkAsRenewed();

            return renewed;
        }

        private void MarkAsRenewed()
        {
            ContractStatus = ContractStatus.Renewed;
            RegisterHistoryEvent("Renewed");
        }

        private void ApplyTerms(RentalPlan rentalPlan, RentalType rentalType, long mileageContracted, decimal totalAmount,
                DateTime pickupDateTime, DateTime? returnDueDateTime)
        {
            var (totalDays, dueDateTime) = CalculatePeriod(rentalType, pickupDateTime, returnDueDateTime);

            RentalPlanId = rentalPlan.Id;
            RentalType = rentalType;
            MileageContracted = mileageContracted;
            EndMileage = CalculateEndMileage(StartMileage, mileageContracted);
            TotalAmount = totalAmount;
            TotalDays = totalDays;
            _pickupDateTime = pickupDateTime;
            _returnDueDateTime = dueDateTime;
            SnapshotPriceDailyRate = rentalPlan.DailyPrice;
            SnapshotPriceMonthlyRate = rentalPlan.MonthlyPrice;
            SnapshotPricePerExtraMileage = rentalPlan.ExcessMileageRate;
        }

        /// <summary>
        /// Fonte única de verdade sobre quantos dias um contrato dura e qual a data de devolução prevista.
        /// Diária: exige returnDueDateTime informado pelo solicitante (>= pickup).
        /// Mensal: ignora returnDueDateTime informado e sempre fecha em 30 dias corridos.
        /// Exposto como público/estático para os use cases poderem usar o mesmo cálculo (ex.: para
        /// derivar km/valor padrão do plano) sem duplicar a regra — o Contract nunca recebe totalDays pronto.
        /// </summary>
        public static (int TotalDays, DateTime ReturnDueDateTime) CalculatePeriod(RentalType rentalType, DateTime pickupDateTime, DateTime? returnDueDateTime)
        {
            if (rentalType == RentalType.Daily)
            {
                if (!returnDueDateTime.HasValue)
                    throw new BusinessRuleException(ResourceErrorMessages.RETURN_DUE_DATE_REQUIRED);

                var returnDue = returnDueDateTime.Value;

                if (returnDue <= pickupDateTime)
                    throw new BusinessRuleException(ResourceErrorMessages.RETURN_DUE_DATE_MUST_BE_AFTER_PICKUP);

                // Cada dia é um bloco de 24h exatas a partir do horário da retirada (ex.: saiu hoje 10:30,
                // só fecha o 1º dia amanhã 10:30). TimeSpan.Days truncava a parte de horas (ex.: 23h virava
                // 0 dias); aqui qualquer fração de 24h conta como um dia cheio a mais (mínimo de 1 dia).
                var totalHours = (returnDue - pickupDateTime).TotalHours;
                var totalDays = (int)Math.Ceiling(totalHours / 24);

                return (totalDays, returnDue);
            }

            return (30, pickupDateTime.AddDays(30));
        }

        private static long CalculateEndMileage(long startMileage, long mileageContracted)
        {
            return startMileage + mileageContracted;
        }
    }
}