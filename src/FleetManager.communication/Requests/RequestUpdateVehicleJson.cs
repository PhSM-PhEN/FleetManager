namespace FleetManager.Communication.Requests
{
    public class RequestUpdateVehicleJson
    {
        
        public string Model { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public long CategoryId { get; set; } 

    }
}
