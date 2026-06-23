using FleetManager.Domain.DomainExceptionBase;
using FleetManager.Domain.Enums;
namespace FleetManager.Domain.Entities
{
    public class Rental : AuditableEntity
    {
        
        public long CompanyId { get; private set; }
        public long ClientId { get; private set; }
        public long VehicleId { get; private set; }
        public long UserId { get; private set; }
        public decimal TotalPrice { get; private set; }
        public long RentalPlanId { get; private set; }
        public RentalMode SnapshotMode { get; private set; }
        public int TotalDays { get; private set; }
        public decimal IncludedKm { get; private set; }
        public decimal SnapshotPriceRental { get; private set; }
        public decimal SnapshotPricePerKm { get; private set; }
        public RentalStatus Status { get; private set; } = RentalStatus.Active;

        public RentalPlan RentalPlan { get; set; } = default!;
        public Company Company { get; set; } = default!;
        public Client Client { get; set; } = default!;
        public Vehicle Vehicle { get; set; } = default!;
        public User User { get; set; } = default!;

        private DateTime _startDate;
        private DateTime _endDate;

        public DateTime StartDate { get => _startDate; private set { _startDate = value; } }
        public DateTime EndDate { get => _endDate; private set { _endDate = value; } }

        protected Rental() { }

        public Rental(long companyId, long clientId, long vehicleId, long userId,
                      DateTime startDate, DateTime endDate)
        {
            CompanyId = companyId;
            ClientId = clientId;
            VehicleId = vehicleId;
            UserId = userId;
            _startDate = startDate;
            _endDate = endDate;
            RecalculateIfReady();
        }

        public void Reschedule(DateTime newStartDate, DateTime newEndDate)
        {
            if (Status != RentalStatus.Active)
                throw new DomainRuleException(ResourceMessages.CANNOT_RESCHEDULE_NON_ACTIVE_RENTAL);

            _startDate = newStartDate;
            _endDate = newEndDate;
            RecalculateIfReady();
        }

        public void Cancel()
        {
            if (Status == RentalStatus.Completed)
                throw new DomainRuleException(ResourceMessages.CANNOT_CANCEL_COMPLETED_RENTAL);

            Status = RentalStatus.Cancelled;
        }

        public void Complete()
        {
            if (Status == RentalStatus.Cancelled)
                throw new DomainRuleException(ResourceMessages.CANNOT_COMPLETE_CANCELLED_RENTAL);

            Status = RentalStatus.Completed;
        }

        public void MarkAsOverdue()
        {
            if (Status != RentalStatus.Active)
                throw new DomainRuleException(ResourceMessages.RENTAL_CANNOT_BE_MARKED_OVERDUE);

            Status = RentalStatus.Overdue;
        }

        private void RecalculateIfReady()
        {
            if (_startDate == default || _endDate == default || SnapshotPriceRental == 0)
                return;

            var days = CalculateTotalDays();
            CalculateTotalPrice(days);
        }

        public void AttachPlan(RentalPlan plan)
        {
            if (plan is null)
                throw new DomainRuleException(ResourceMessages.RENTAL_PLAN_CANNOT_BE_NULL);

            if (!plan.IsActive)
                throw new DomainRuleException(ResourceMessages.RENTAL_PLAN_IS_NOT_ACTIVE);

            RentalPlanId = plan.Id;
            SnapshotPriceRental = plan.PriceRental;
            SnapshotPricePerKm = plan.PricePerKm;
            SnapshotMode = plan.Mode;

            RecalculateIfReady();
        }

        public void UpdateIncludedKm(decimal newKm)
        {
            if (newKm < 0)
                throw new DomainRuleException(ResourceMessages.INCLUDED_KM_CANNOT_BE_NEGATIVE);

            IncludedKm = newKm;
            var days = CalculateTotalDays();
            CalculateTotalPrice(days);
        }

        private int CalculateTotalDays()
        {
            if (_endDate < _startDate)
                throw new DomainRuleException(ResourceMessages.END_DATE_MUST_BE_GREATER_THAN_START_DATE);

            return (_endDate - _startDate).Days;
        }

        private void CalculateTotalPrice(int days)
        {
            TotalDays = days;
            decimal basePrice = SnapshotMode == RentalMode.Monthly
            ? Math.Floor(days / 30m) * SnapshotPriceRental : 
            days * SnapshotPriceRental ; 
        

            decimal kmPrice = IncludedKm > 0
                ? IncludedKm * SnapshotPricePerKm
                : 0;

            TotalPrice = basePrice + kmPrice;
        }
    }
}