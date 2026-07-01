using FleetManager.Domain.DomainExceptionBase;
using FleetManager.Domain.Enums;

namespace FleetManager.Domain.Entities
{
    public class RentalPlan : AuditableEntity
    {
       
        public string Name { get; private set; } = string.Empty;
        public RentalMode Mode { get; private set; }
        public TransmissionType Transmission { get; private set; }
        public decimal PriceRental { get; private set; }
        public long TotalKmIncluded {get ; private set ; }
        public decimal PricePerKm { get; private set; }
        public bool IsActive { get; private set; } = true;

        public void Update(string name, RentalMode mode, TransmissionType transmission,
                           decimal priceRental, long totalKmIncluded, decimal pricePerKm)
        {
            Name = name;
            Mode = mode;
            Transmission = transmission;
            TotalKmIncluded = totalKmIncluded;
            UpdatePrices(priceRental, pricePerKm); 
        }

        protected RentalPlan() { }

        public RentalPlan(string name, RentalMode mode, TransmissionType transmission, decimal priceRental, long totalKmIncluded , decimal pricePerKm)
        {
            Name = name;
            Mode = mode;
            Transmission = transmission;
            PriceRental = priceRental;
            TotalKmIncluded = totalKmIncluded;
            PricePerKm = pricePerKm;
        }
        public void Disable()
        {
            IsActive = false;
        }
        public void Activate()
        {
            IsActive = true;
        }
        private void UpdatePrices(decimal priceRental, decimal pricePerKm)
        {
            if (priceRental < 0)
                throw new DomainRuleException(ResourceMessages.PRICE_CANNOT_BE_NEGATIVE);
            if (pricePerKm < 0)
                throw new DomainRuleException(ResourceMessages.PRICE_CANNOT_BE_NEGATIVE);

            PriceRental = priceRental;
            PricePerKm = pricePerKm;
        }


    }
}
