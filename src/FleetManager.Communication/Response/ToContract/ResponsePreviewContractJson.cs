namespace FleetManager.Communication.Response.ToContract
{
    public class ResponsePreviewContractJson
    {
        public long VehicleId { get; set; }
        public long TenantId { get; set; }
        public long RentalPlanId { get; set; }
        public string RentalType { get; set; } = string.Empty;
        public DateTime PickupDateTime { get; set; }
        public DateTime ReturnDueDateTime { get; set; }
        public int TotalDays { get; set; }
        public long MileageContracted { get; set; }
        public decimal TotalAmount { get; set; }
        public ResponseEnumStatusJson Status {get ; set ;} = new();
    }
}
