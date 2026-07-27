using FleetManager.Communication.Enum;

namespace FleetManager.Communication.Request.ToContract
{
    public class RequestContractJson
    {
        public long VehicleId { get; set; }
        public long TenantId { get; set; }
        public RentalType RentalType { get; set; }  // "Daily" ou "Monthly"
        public DateTime PickupDateTime { get; set; }
        public DateTime? ReturnDueDateTime { get; set; }        // obrigatório só se RentalType = Daily
        public long? AdditionalKilometers { get; set; }
    }
}