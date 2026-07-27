using FleetManager.Communication.Response.ToRenant;
using FleetManager.Communication.Response.ToVehicle;

namespace FleetManager.Communication.Response.ToContract
{
    public class ResponseContractJson
    {
        public long? Id { get; set; }                 // nulo na prévia, preenchido no registro
        public string RentalType { get; set; } = string.Empty;
        public DateTime PickupDateTime { get; set; }
        public DateTime ReturnDueDateTime { get; set; }
        public int TotalDays { get; set; }
        public long MileageAllowance { get; set; }
        public decimal BaseRentalAmount { get; set; }
        public decimal AdditionalKilometersAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public ResponseShortVehicleJson Vehicle { get; set; } = new();
        public ResponseShortTenantJson Tenant { get; set; } = new();
    }
}