namespace FleetManager.Communication.Response.ToVehicle
{
    public class ResponseVehicleRegisteredJson
    {
        public long Id { get; set; }
        public string LicensePlate { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public long CurrentMileage { get; set; }
    }
}
