using FleetManager.Domain.Enum;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Domain.Entities
{
    public class Contract : AudiTableEntity
    {
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

        private DateTime _pickupDateTime;
        private DateTime _returnDueDateTime;
        public DateTime PickupDateTime { get => _pickupDateTime; private set => _pickupDateTime = value; }
        public DateTime ReturnDueDateTime { get => _returnDueDateTime; private set => _returnDueDateTime = value; }

        public ContractStatus ContractStatus { get; private set; } = ContractStatus.Active;
        public DateTime? ActualReturnDateTime { get; private set; }

        public Vehicle Vehicle { get; set; } = default!;
        public Tenant Tenant { get; set; } = default!;
        public RentalPlan RentalPlan { get; set; } = default!;

        protected Contract() { }

        // Usado no registro original — sempre tira o snapshot do RentalPlan vigente no momento.
        public Contract(long vehicleId, long tenantId, RentalPlan rentalPlan, RentalType rentalType, long startMileage,
                        long mileageContracted, decimal totalAmount, int totalDays, DateTime pickupDateTime, DateTime returnDueDateTime)
                        
            : this(vehicleId, tenantId, rentalPlan.Id, rentalType, startMileage, mileageContracted, totalAmount, totalDays,
                   pickupDateTime, returnDueDateTime, rentalPlan.DailyPrice, rentalPlan.MonthlyPrice, rentalPlan.ExcessMileageRate)
        {
        }

        // Construtor interno — recebe os valores de snapshot já decididos por fora (usado pela renovação também).
        private Contract(long vehicleId, long tenantId, long rentalPlanId, RentalType rentalType, long startMileage,
                        long mileageContracted, decimal totalAmount, int totalDays, DateTime pickupDateTime, DateTime returnDueDateTime,
                        decimal snapshotDailyRate, decimal snapshotMonthlyRate, decimal snapshotExcessRate)
        {
            VehicleId = vehicleId;
            TenantId = tenantId;
            RentalPlanId = rentalPlanId;
            RentalType = rentalType;
            StartMileage = startMileage;
            MileageContracted = mileageContracted;
            EndMileage = CalculateEndMileage(startMileage, mileageContracted);
            TotalAmount = totalAmount;
            TotalDays = totalDays;
            _pickupDateTime = pickupDateTime;
            _returnDueDateTime = returnDueDateTime;
            SnapshotPriceDailyRate = snapshotDailyRate;
            SnapshotPriceMonthlyRate = snapshotMonthlyRate;
            SnapshotPricePerExtraMileage = snapshotExcessRate;
        }

        public void Cancel()
        {
            if (ContractStatus != ContractStatus.Active)
                throw new BusinessRuleException(ResourceErrorMessages.CONTRACT_NOT_ACTIVE);

            ContractStatus = ContractStatus.Cancelled;
        }

        public void Complete(DateTime actualReturnDateTime)
        {
            if (ContractStatus != ContractStatus.Active && ContractStatus != ContractStatus.Overdue)
                throw new BusinessRuleException(ResourceErrorMessages.CONTRACT_NOT_ACTIVE);

            ActualReturnDateTime = actualReturnDateTime;
            ContractStatus = ContractStatus.Finished;
        }

        public void MarkAsOverdue()
        {
            if (ContractStatus != ContractStatus.Active)
                throw new BusinessRuleException(ResourceErrorMessages.CONTRACT_NOT_ACTIVE);

            ContractStatus = ContractStatus.Overdue;
        }

        // Gera um NOVO contrato a partir de um vigente, em vez de alterar o atual.
        // newRentalPlan/mileageContractedOverride nulos = repete o que já estava congelado no anterior.
        public static Contract Renew(Contract previousContract, RentalPlan? newRentalPlan, long? mileageContractedOverride)
        {
            if (previousContract.ContractStatus != ContractStatus.Active)
                throw new BusinessRuleException(ResourceErrorMessages.CONTRACT_NOT_ACTIVE);

            if (DateTime.UtcNow >= previousContract.ReturnDueDateTime)
                throw new BusinessRuleException(ResourceErrorMessages.RENEWAL_MUST_BE_REQUESTED_BEFORE_DUE_DATE);

            var mileageContracted = mileageContractedOverride ?? previousContract.MileageContracted;
            var pickupDateTime = previousContract.ReturnDueDateTime;               // começa onde o anterior deveria terminar
            var returnDueDateTime = pickupDateTime.AddDays(previousContract.TotalDays);
            var startMileage = previousContract.EndMileage;                        // idem, pro km

            var dailyRate = newRentalPlan?.DailyPrice ?? previousContract.SnapshotPriceDailyRate;
            var monthlyRate = newRentalPlan?.MonthlyPrice ?? previousContract.SnapshotPriceMonthlyRate;
            var excessRate = newRentalPlan?.ExcessMileageRate ?? previousContract.SnapshotPricePerExtraMileage;
            var rentalPlanId = newRentalPlan?.Id ?? previousContract.RentalPlanId;

            var totalAmount = previousContract.RentalType == RentalType.Daily
                ? dailyRate * previousContract.TotalDays
                : monthlyRate;

            var renewed = new Contract(previousContract.VehicleId, previousContract.TenantId, rentalPlanId, previousContract.RentalType,
                startMileage, mileageContracted, totalAmount, previousContract.TotalDays, pickupDateTime, returnDueDateTime,
                dailyRate, monthlyRate, excessRate);

            previousContract.MarkAsRenewed();

            return renewed;
        }

        private void MarkAsRenewed()
        {
            ContractStatus = ContractStatus.Renewed;
        }

        private static long CalculateEndMileage(long startMileage, long mileageContracted)
        {
            return startMileage + mileageContracted;
        }
    }
}