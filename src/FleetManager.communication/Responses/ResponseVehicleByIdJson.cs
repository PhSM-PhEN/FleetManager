namespace FleetManager.communication.Responses
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
        public long CurrentMileage { get; set; }
        public DateTime CreateAt { get;  set; }
        
        public ResponseCategoryJson Category { get; set; } = default!;

    }
}
