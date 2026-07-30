namespace FleetManager.Communication.Request.ToContract
{
    public class RequestContractJson
    {
        public long VehicleId { get; set; }
        public long TenantId { get; set; }
        public long RentalPlanId { get; set; }
        public string RentalType { get; set; } = string.Empty;
        public long MileageContracted { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime PickupDateTime { get; set; }
        public DateTime? ReturnDueDateTime { get; set; }
    }
}