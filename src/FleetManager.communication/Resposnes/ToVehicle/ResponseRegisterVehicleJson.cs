using FleetManager.communication.Enums;

namespace FleetManager.communication.Resposnes.ToVehicle
{
    public class ResponseRegisterVehicleJson
    {
        public long Id { get; set; }
        public string LicensePlate { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public FuelTypeEnum FuelType { get; set; }
        public TransmissionTypeEnum TransmissionType { get; set; }
        public long CurrentMileage { get; set; }
        public int CategoryId { get; set; }

    }
}
