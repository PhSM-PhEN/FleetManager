
namespace FleetManager.Communication.Request.ToContract
{
    public class RequestContractJson
    {
        public long VehicleId { get; set; }
        public long TenantId { get; set; }
        public string RentalType { get; set; } = string.Empty;  // "Daily" ou "Monthly"
        public DateTime PickupDateTime { get; set; }
        public DateTime? ReturnDueDateTime { get; set; }        // obrigatório só se RentalType = Daily
        public long? AdditionalKilometers { get; set; }
    }
}