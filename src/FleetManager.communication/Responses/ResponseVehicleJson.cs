namespace FleetManager.Communication.Responses
{
    public class ResponseVehicleJson
    {
        public long Id { get; set; }
        public string LicensePlate { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public long CurrentMileage { get; set; }
        
    }
}
