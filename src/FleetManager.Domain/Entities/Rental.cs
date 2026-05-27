
using FleetManager.Domain.DomainExceptionBase;
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


        public RentalPlan RentalPlan { get; set; } = default!;
        public Company Company { get; set; } = default!;
        public Client Client { get; set; } = default!;
        public Vehicle Vehicle { get; set; } = default!;
        public User User { get; set; } = default!;
        private DateTime _startDate { get; set; } 
        private DateTime _endDate { get; set; }

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
            throw new DomainRuleException("RentalPlan cannot be null.");

        if (!plan.IsActive)
            throw new DomainRuleException("Cannot attach an inactive RentalPlan.");

        RentalPlanId         = plan.Id;
        RentalPlan           = plan;
        SnapshotPriceRental  = plan.PriceRental;  // ← fotografia
        SnapshotPricePerKm   = plan.PricePerKm;   // ← fotografia
        RecalculateIfReady();
        }
        public void UpdateIncludedKm(decimal newKm)
        {
        if (newKm < 0)
            throw new DomainRuleException("IncludedKm cannot be negative.");

        IncludedKm = newKm;
        CalculateTotalPrice();
        }
        private void CalculateTotalDays()
        {
            if (_endDate < _startDate)
                throw new DomainRuleException("End date must be greater than or equal to start date.");

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
