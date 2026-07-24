namespace FleetManager.Communication.Request.ToVehicle
{
    public class RequestVehicleJson
    {
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string ManufacturingYear { get; set; } = string.Empty;   // ex: "2024/2025"
        public string Renavam { get; set; } = string.Empty;
        public string ChassiNumber { get; set; } = string.Empty;
        public string LicensePlate { get; set; } = string.Empty;
        public long CurrentMileage { get; set; }
        public long CompanyId { get; set; }
    }
}