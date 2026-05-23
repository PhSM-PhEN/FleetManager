namespace FleetManager.communication.Responses
{
    public class ResponseRegisterVehicleJson
    {
        public long Id { get; set; }
        public string LicensePlate { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public long CurrentMileage { get; set; }
        public int CategoryId { get; set; }

    }
}
