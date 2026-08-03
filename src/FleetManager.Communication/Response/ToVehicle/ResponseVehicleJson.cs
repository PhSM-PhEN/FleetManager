using FleetManager.Communication.Response.ToCompany;
using FleetManager.Communication.Response.ToRentalPlan;

namespace FleetManager.Communication.Response.ToVehicle
{
    public class ResponseVehicleJson
    {
        public long Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string ManufacturingYear { get; set; } = string.Empty;   
        public string Renavam { get; set; } = string.Empty;
        public string ChassiNumber { get; set; } = string.Empty;
        public string LicensePlate { get; set; } = string.Empty;
        public long CurrentMileage { get; set; }
        public ResponseCompanyJson Company { get; set; } = default!;
        public ResponseRentalPlanJson RentalPlan { get; set; } = default!;
    }
}
