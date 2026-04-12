using FleetManager.communication.Enums;

namespace FleetManager.communication.Resposnes
{
    public class ResponseVehicleByIdJson
    {
        public long Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int ManufacturingYear { get; set; }
        public string Renavam { get; set; } = string.Empty;
        public string LicensePlate { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public int CurrentMileage { get; set; }
        public FuelTypeEnum FuelType { get; set; }
        public TransmissionTypeEnum TransmissionType { get; set; }
        public DateTime CreateAt { get;  set; } 
        public ResponseShortCategoryJson Category { get; set; } = default!;

    }
}
