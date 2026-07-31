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

        public ContractStatus ContractStatus { get; private set; } = ContractStatus.Reserved;
        public DateTime? ActualReturnDateTime { get; private set; }

        public Vehicle Vehicle { get; set; } = default!;
        public Tenant Tenant { get; set; } = default!;
        public RentalPlan RentalPlan { get; set; } = default!;

        protected Contract() { }

        public Contract(long vehicleId, long tenantId, RentalPlan rentalPlan, RentalType rentalType, long startMileage,
                        long mileageContracted, decimal totalAmount, int totalDays, DateTime pickupDateTime, DateTime returnDueDateTime)
        {
            VehicleId = vehicleId;
            TenantId = tenantId;
            StartMileage = startMileage;
            ApplyTerms(rentalPlan, rentalType, mileageContracted, totalAmount, totalDays, pickupDateTime, returnDueDateTime);
        }

        public void Cancel()
        {
            if (ContractStatus != ContractStatus.Active && ContractStatus != ContractStatus.Reserved)
                throw new BusinessRuleException(ResourceErrorMessages.CONTRACT_NOT_ACTIVE);

            ContractStatus = ContractStatus.Cancelled;
        }

        public void Confirm()
        {
            if (ContractStatus != ContractStatus.Reserved)
                throw new BusinessRuleException("ResourceErrorMessages.CONTRACT_NOT_RESERVED");

            ContractStatus = ContractStatus.Active;
        }

        public void Update(RentalPlan rentalPlan, RentalType rentalType, long mileageContracted, decimal totalAmount,
                        int totalDays, DateTime pickupDateTime, DateTime returnDueDateTime)
        {
            if (ContractStatus != ContractStatus.Reserved)
                throw new BusinessRuleException("ResourceErrorMessages.CONTRACT_NOT_EDITABLE");

            ApplyTerms(rentalPlan, rentalType, mileageContracted, totalAmount, totalDays, pickupDateTime, returnDueDateTime);
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

        public static Contract Renew(Contract previousContract, RentalPlan? newRentalPlan, long? mileageContractedOverride)
        {
            if (previousContract.ContractStatus != ContractStatus.Active)
                throw new BusinessRuleException(ResourceErrorMessages.CONTRACT_NOT_ACTIVE);

            if (DateTime.UtcNow >= previousContract.ReturnDueDateTime)
                throw new BusinessRuleException(ResourceErrorMessages.RENEWAL_MUST_BE_REQUESTED_BEFORE_DUE_DATE);

            var mileageContracted = mileageContractedOverride ?? previousContract.MileageContracted;
            var pickupDateTime = previousContract.ReturnDueDateTime;
            var returnDueDateTime = pickupDateTime.AddDays(previousContract.TotalDays);
            var startMileage = previousContract.EndMileage;

            var plan = newRentalPlan ?? previousContract.RentalPlan;

            var totalAmount = previousContract.RentalType == RentalType.Daily
                ? (newRentalPlan?.DailyPrice ?? previousContract.SnapshotPriceDailyRate) * previousContract.TotalDays
                : (newRentalPlan?.MonthlyPrice ?? previousContract.SnapshotPriceMonthlyRate);

            var renewed = new Contract(previousContract.VehicleId, previousContract.TenantId, plan, previousContract.RentalType,
                startMileage, mileageContracted, totalAmount, previousContract.TotalDays, pickupDateTime, returnDueDateTime);

            renewed.Confirm(); // uma renovação já nasce confirmada — não faz sentido reservar de novo o que já estava rodando
            previousContract.MarkAsRenewed();

            return renewed;
        }

        private void MarkAsRenewed()
        {
            ContractStatus = ContractStatus.Renewed;
        }

        private void ApplyTerms(RentalPlan rentalPlan, RentalType rentalType, long mileageContracted, decimal totalAmount,
                        int totalDays, DateTime pickupDateTime, DateTime returnDueDateTime)
        {
            RentalPlanId = rentalPlan.Id;
            RentalType = rentalType;
            MileageContracted = mileageContracted;
            EndMileage = CalculateEndMileage(StartMileage, mileageContracted);
            TotalAmount = totalAmount;
            TotalDays = totalDays;
            _pickupDateTime = pickupDateTime;
            _returnDueDateTime = returnDueDateTime;
            SnapshotPriceDailyRate = rentalPlan.DailyPrice;
            SnapshotPriceMonthlyRate = rentalPlan.MonthlyPrice;
            SnapshotPricePerExtraMileage = rentalPlan.ExcessMileageRate;
        }

        private static long CalculateEndMileage(long startMileage, long mileageContracted)
        {
            return startMileage + mileageContracted;
        }
    }
}