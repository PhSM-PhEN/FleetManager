using FleetManager.Domain.DomainExceptionBase;

namespace FleetManager.Domain.Entities
{
    public class Vehicle : AuditableEntity
    {
        
        public string Brand { get; private set; } = string.Empty;
        public string Model { get; private set; } = string.Empty;
        public int ManufacturingYear { get; private set; }
        public string Renavam { get; private set; } = string.Empty;
        public string ChassisNumber { get; private set; } = string.Empty;
        public string LicensePlate { get; private set; } = string.Empty;
        public string Color { get; private set; } = string.Empty;
        public long CategoryId { get; private set; }
        public decimal CurrentMileage { get; private set; }
        public DateTime CreateAt { get; private set; } = DateTime.UtcNow;
        public bool IsActive { get; private set; } = true;
        public Category Category { get; set; } = default!;

        protected Vehicle() { }

        public Vehicle(string brand, string model, int manufacturingYear, string renavam,
                       string chassisNumber, string licensePlate, string color,
                       long categoryId, decimal currentMileage)
        {
            Brand = brand;
            Model = model;
            ManufacturingYear = manufacturingYear;
            Renavam = renavam;
            ChassisNumber = chassisNumber;
            LicensePlate = licensePlate;
            Color = color;
            CategoryId = categoryId;
            CurrentMileage = currentMileage;
        }

        public void Update(string brand, string model, string color, long categoryId)
        {
            Brand = brand;
            Model = model;
            Color = color;
            CategoryId = categoryId;
        }

        public void UpdateCurrentMileage(decimal newMileage)
        {
            if (newMileage < CurrentMileage)
                throw new DomainRuleException(ResourceMessages.THE_MILEAGE_MUST_BE_HIGHER_THAN_THE_CURRENT);

            CurrentMileage = newMileage;
        }

        public void Disable() => IsActive = false;
        public void Activate() => IsActive = true;
    }
}

