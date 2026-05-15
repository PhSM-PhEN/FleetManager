using FleetManager.Domain.DomainExceptionBase;
using FleetManager.Exception.ExceptionBase;

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
        public long CurrentMileage { get; set; }
        public DateTime CreateAt { get; private set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
        public int CategoryId { get; set; }
        public Category Category { get; set; } = default!;


        public void UpdateCurrentMileage(long newMileage)
        {
            if (newMileage < CurrentMileage)
            {
                throw new DomainRuleException(ResourceErrorMessages.THE_MILEAGE_MUST_BE_HIGHER_THAN_THE_CURRENT);
            }       
            
            CurrentMileage = newMileage;
            
        }
    }
}
