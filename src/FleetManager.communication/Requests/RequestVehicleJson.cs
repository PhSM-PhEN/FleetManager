namespace FleetManager.Communication.Requests
{
    public class RequestVehicleJson
    {

        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int ManufacturingYear { get; set; }
        public string Renavam { get; set; } = string.Empty;
        public string ChassisNumber { get; set; } = string.Empty;
        public string LicensePlate { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public decimal CurrentMileage { get; set; }
        public long CategoryId { get; set; }
    }
}
