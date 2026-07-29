namespace FleetManager.Communication.Request.ToContract
{
    public class RequestContractJson
    {
        public long VehicleId { get; set; }
        public long TenantId { get; set; }
        public long RentalPlanId { get; set; }
        public string RentalType { get; set; } = string.Empty; // "Daily" ou "Monthly"
        public DateTime PickupDateTime { get; set; }
        public DateTime? ReturnDueDateTime { get; set; }        // obrigatório só se RentalType = Daily
        public long? MileageContracted { get; set; }         // km extra além da franquia do plano, opcional
    }
}