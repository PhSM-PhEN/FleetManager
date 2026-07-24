namespace FleetManager.Communication.Response.ToVehicle
{
    public class ResponseShortVehicleJson
    {
        public long Id {get ; set ;}
        public string Model { get; set; } = string.Empty;
        public string LicensePlate {get ; set ;} = string.Empty;
        public long CurrentMileage { get; set; }
    }
}
