namespace FleetManager.Communication.Request.ToContract
{
    public class RequestPreviewContractJson
    {
        public long VehicleId { get; set; }
        public long TenantId { get; set; }
        public string RentalType { get; set; } = string.Empty;
        public DateTime PickupDateTime { get; set; }
        public DateTime? ReturnDueDateTime { get; set; }
    }
}
