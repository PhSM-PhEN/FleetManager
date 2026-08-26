namespace FleetManager.Communication.Response.ToVehicle
{
    public class ResponseRegisterVehicleJson
    {
        public long Id { get; set; }
        public string LicensePlate { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public long CurrentMileage { get; set; }
        public ResponseEnumStatusJson Status { get; set; } = new();
    }
}
