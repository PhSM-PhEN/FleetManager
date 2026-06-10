using FleetManager.Domain.DomainExceptionBase;

namespace FleetManager.Domain.Entities
{
    public class Vehicle
    {

        public long Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int ManufacturingYear { get; set; }
        public string Renavam { get; set; } = string.Empty;
        public string ChassisNumber { get; set; } = string.Empty;
        public string LicensePlate { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public decimal CurrentMileage { get; private set; }
        public DateTime CreateAt { get; private set; } = DateTime.UtcNow;
        public Category Category { get; set; } = default!;
        public bool IsActive { get; private set; } = true;
        public void UpdateCurrentMileage(decimal newMileage)
        {
            if (newMileage < CurrentMileage)
            {
                throw new DomainRuleException(ResourceMessages.THE_MILEAGE_MUST_BE_HIGHER_THAN_THE_CURRENT);
            }

            CurrentMileage = newMileage;
        }

        public void Disable()
        {
            IsActive = false;
        }
      
        public void Activate()
        {
            IsActive = true;
        }
    }
}

