
using FleetManager.Domain.DomainExceptionBase;
using FleetManager.Domain.Enums;
namespace FleetManager.Domain.Entities
{
    public class Rental
    {
        public long Id { get; set; }
        public int CompanyId { get; set; }
        public long ClientId { get; set; }
        public long VehicleId { get; set; }
        public long UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public int RentalPlanId { get; set; }
        public int TotalDays {get ; private set ;}
        public decimal IncludedKm {get ; set;}
        public decimal SnapshotPriceRental {get; private set;}
        public decimal SnapshotPricePerKm {get; set ;}
        public RentalStatus Status { get; private set; } = RentalStatus.Active;


        public RentalPlan RentalPlan { get; set; } = default!;
        public Company Company { get; set; } = default!;
        public Client Client { get; set; } = default!;
        public Vehicle Vehicle { get; set; } = default!;
        public User User { get; set; } = default!;

        private DateTime _startDate ;

        private DateTime _endDate ;

        public DateTime StartDate
        {
            get => _startDate;

            set
            {
                _startDate = value; 
                RecalculateIfReady();

            }
        }
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                RecalculateIfReady();
            }
        }
        public void Cancel()
        {
            if (Status == RentalStatus.Completed)
            {
                throw new DomainRuleException(ResourceMessages.CANNOT_CANCEL_COMPLETED_RENTAL);
            }
            Status = RentalStatus.Cancelled;
        }
        public void Complete()
        {
            if (Status == RentalStatus.Cancelled)
            {
                throw new DomainRuleException(ResourceMessages.CANNOT_COMPLETE_CANCELLED_RENTAL);
            }
            Status = RentalStatus.Completed;
        }
        public void Overdue()
        {
            if (Status != RentalStatus.Active)
                throw new DomainRuleException(ResourceMessages.RENTAL_CANNOT_BE_MARKED_OVERDUE);
            Status = RentalStatus.Overdue;
        }


        private void RecalculateIfReady()
        {
            if(_startDate == default || _endDate == default || SnapshotPriceRental == 0)
            {
                return ;
            }
            CalculateTotalDays();
            CalculateTotalPrice();
        }
        public void AttachPlan(RentalPlan plan)
        {
        if (plan is null)
            throw new DomainRuleException(ResourceMessages.RENTAL_PLAN_CANNOT_BE_NULL);

        if (!plan.IsActive)
            throw new DomainRuleException(ResourceMessages.RENTAL_PLAN_IS_NOT_ACTIVE);

        RentalPlanId         = plan.Id;
        SnapshotPriceRental  = plan.PriceRental;  
        SnapshotPricePerKm   = plan.PricePerKm;   
        RecalculateIfReady();
        }
        public void UpdateIncludedKm(decimal newKm)
        {
        if (newKm < 0)
            throw new DomainRuleException(ResourceMessages.INCLUDED_KM_CANNOT_BE_NEGATIVE);

        IncludedKm = newKm;
        CalculateTotalPrice();
        }
        private void CalculateTotalDays()
        {
            if (_endDate < _startDate)
                throw new DomainRuleException(ResourceMessages.END_DATE_MUST_BE_GREATER_THAN_START_DATE);

            TotalDays = (_endDate - _startDate).Days;
            
            
        }
        private void CalculateTotalPrice()
        {
            decimal basePrice = TotalDays * SnapshotPriceRental;
            decimal kmPrice = IncludedKm > 0
            ? IncludedKm * SnapshotPricePerKm : 0 ;

            TotalPrice = basePrice + kmPrice;
        }

    }

}
