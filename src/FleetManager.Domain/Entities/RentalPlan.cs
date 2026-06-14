using FleetManager.Domain.DomainExceptionBase;
using FleetManager.Domain.Enums;

namespace FleetManager.Domain.Entities
{
    public class RentalPlan
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public RentalMode Mode { get; set; }
        public TransmissionType Transmission { get; set; }
        public decimal PriceRental { get;  set; }
        public decimal PricePerKm { get;  set; }
        public bool IsActive { get; set; } = true;

        protected RentalPlan() { }

        public RentalPlan(string name, RentalMode mode, TransmissionType transmission, decimal priceRental, decimal pricePerKm)
        {
            Name = name;
            Mode = mode;
            Transmission = transmission;
            PriceRental = priceRental;
            PricePerKm = pricePerKm;
        }
        public void Update(string name, RentalMode mode, TransmissionType transmission, decimal priceRental, decimal pricePerKm)
        {
            Name = name;
            Mode = mode;
            Transmission = transmission;
            PriceRental = priceRental;
            PricePerKm = pricePerKm;
            UpdatePrices(priceRental, pricePerKm);


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
