namespace FleetManager.Communication.Requests
{
    public class RequestUpdateVehicleJson
    {
        public int Id { get; set; }
        public string Model { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public int CategoryId { get; set; } 

    }
}
