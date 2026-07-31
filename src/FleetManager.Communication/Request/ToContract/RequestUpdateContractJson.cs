namespace FleetManager.Communication.Request.ToContract
{
    public class RequestUpdateContractJson
    {
        public long MileageContracted { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime PickupDateTime { get; set; }
        public DateTime? ReturnDueDateTime { get; set; }
    }
}
