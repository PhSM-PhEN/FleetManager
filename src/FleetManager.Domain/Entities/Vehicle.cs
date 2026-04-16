using FleetManager.Domain.Enums;

namespace FleetManager.Domain.Entities
{
    public class Vehicle
    {
        public long Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int ManufacturingYear { get; set; }
        public string Renavam { get; set; } = string.Empty;
        public string LicensePlate { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public long CurrentMileage { get; set; }
        public FuelTypeEnum FuelType { get; set; }
        public TransmissionTypeEnum TransmissionType { get; set; }
        public DateTime CreateAt { get; private set; } = DateTime.UtcNow;
        public int CategoryId { get; set; }
        public Category Category { get; set; } = default!;

    }
}
