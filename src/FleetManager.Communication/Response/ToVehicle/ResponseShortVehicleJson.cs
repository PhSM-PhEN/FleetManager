using FleetManager.Communication.Response.ToCompany;

namespace FleetManager.Communication.Response.ToVehicle
{
    public class ResponseShortVehicleJson
    {
        public long Id {get ; set ;}
        public string Model { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string ManufacturingYear { get; set; } = string.Empty;
        public string ChassiNumber { get; set; } = string.Empty;
        public string LicensePlate {get ; set ;} = string.Empty;
        public long CurrentMileage { get; set; }
        public ResponseCompanyJson Company { get; set; } = new();
    }   
}
