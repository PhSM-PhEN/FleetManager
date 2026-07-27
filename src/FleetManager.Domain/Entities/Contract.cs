using FleetManager.Domain.Enum;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Domain.Entities
{
    public class Contract : AudiTableEntity
    {
        public long VehicleId { get; private set; }
        public long TenantId { get; private set; }
        public RentalType RentalType { get; private set; }

        public long StartMileage { get; private set; }
        public long SnapshotMileageAllowed { get; private set; }

        public decimal SnapshotPriceDailyRate { get; private set; }
        public decimal SnapshotPriceMonthlyRate { get; private set; }
        public decimal AgreedExcessMileageRate { get; private set; }
        public long? AgreedAdditionalKilometers { get; private set; }

        public decimal BaseRentalAmount { get; private set; }
        public decimal AdditionalKilometersAmount { get; private set; }
        public decimal TotalAmount { get; private set; }

        public int TotalDays { get; private set; }

        private DateTime _pickupDateTime;
        private DateTime _returnDueDateTime;
        public DateTime PickupDateTime { get => _pickupDateTime; private set => _pickupDateTime = value; }
        public DateTime ReturnDueDateTime { get => _returnDueDateTime; private set => _returnDueDateTime = value; }

        public ContractStatus ContractStatus { get; private set; } = ContractStatus.Active;

        public long? EndMileage { get; private set; }
        public DateTime? ActualReturnDateTime { get; private set; }
        public long PeriodMileageAllowance { get; private set; } // franquia do período, sem contar km adicional

        public Vehicle Vehicle { get; set; } = default!;
        public Tenant Tenant { get; set; } = default!;

        protected Contract() { }

        public Contract(long vehicleId, long tenantId, RentalType rentalType, long startMileage,
                         long snapshotMileageAllowed, decimal snapshotPriceDailyRate, decimal snapshotPriceMonthlyRate,
                         decimal agreedExcessMileageRate, long? agreedAdditionalKilometers,
                         decimal baseRentalAmount, decimal additionalKilometersAmount,
                         int totalDays, DateTime pickupDateTime, DateTime returnDueDateTime)
        {
            VehicleId = vehicleId;
            TenantId = tenantId;
            RentalType = rentalType;
            StartMileage = startMileage;
            SnapshotMileageAllowed = snapshotMileageAllowed;
            SnapshotPriceDailyRate = snapshotPriceDailyRate;
            SnapshotPriceMonthlyRate = snapshotPriceMonthlyRate;
            AgreedExcessMileageRate = agreedExcessMileageRate;
            AgreedAdditionalKilometers = agreedAdditionalKilometers;
            BaseRentalAmount = baseRentalAmount;
            AdditionalKilometersAmount = additionalKilometersAmount;
            TotalAmount = baseRentalAmount + additionalKilometersAmount;
            TotalDays = totalDays;
            _pickupDateTime = pickupDateTime;
            _returnDueDateTime = returnDueDateTime;
        }
        public void Cancel()
        {
            if (ContractStatus != ContractStatus.Active)
                throw new BusinessRuleException(ResourceErrorMessages.CONTRACT_NOT_ACTIVE);

            ContractStatus = ContractStatus.Cancelled;
        }
        public void Complete(long endMileage, DateTime actualReturnDateTime)
        {
            if (ContractStatus != ContractStatus.Active && ContractStatus != ContractStatus.Overdue)
                throw new BusinessRuleException(ResourceErrorMessages.CONTRACT_NOT_ACTIVE);

            if (endMileage < StartMileage)
                throw new BusinessRuleException(ResourceErrorMessages.END_MILEAGE_CANNOT_BE_LESS_THAN_START);

            EndMileage = endMileage;
            ActualReturnDateTime = actualReturnDateTime;
            ContractStatus = ContractStatus.Finished;
        }
        public void MarkAsOverdue()
        {
            if (ContractStatus != ContractStatus.Active)
                throw new BusinessRuleException(ResourceErrorMessages.CONTRACT_NOT_ACTIVE);

            ContractStatus = ContractStatus.Overdue;
        }
        public void Reschedule()
        {
            if (ContractStatus != ContractStatus.Active)
                throw new BusinessRuleException(ResourceErrorMessages.CONTRACT_NOT_ACTIVE);

            if (DateTime.UtcNow >= _returnDueDateTime)
                throw new BusinessRuleException(ResourceErrorMessages.RENEWAL_MUST_BE_REQUESTED_BEFORE_DUE_DATE);

            _returnDueDateTime = _returnDueDateTime.AddDays(TotalDays);
            SnapshotMileageAllowed += PeriodMileageAllowance;
            TotalAmount += BaseRentalAmount;
        }
        public void AddExtraMileage(long additionalMileage, decimal additionalAmount)
        {
            if (ContractStatus != ContractStatus.Active)
                throw new BusinessRuleException(ResourceErrorMessages.CONTRACT_NOT_ACTIVE);

            if (additionalMileage <= 0)
                throw new BusinessRuleException(ResourceErrorMessages.ADDITIONAL_MILEAGE_MUST_BE_POSITIVE);

            AgreedAdditionalKilometers = (AgreedAdditionalKilometers ?? 0) + additionalMileage;
            SnapshotMileageAllowed += additionalMileage;
            AdditionalKilometersAmount += additionalAmount;
            TotalAmount += additionalAmount;
        }
    }
}