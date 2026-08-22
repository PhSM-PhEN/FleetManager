namespace FleetManager.Communication.Request.ToContract
{
    public class RequestPreviewContractJson
    {
        public long VehicleId { get; set; }
        public long TenantId { get; set; }
        public string RentalType { get; set; } = string.Empty;
        public long DesiredExcessMileage { get ; set ;}
        public DateTime PickupDateTime { get; set; }
        public DateTime? ReturnDueDateTime { get; set; }
    }
}
