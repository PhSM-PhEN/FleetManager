
namespace FleetManager.Communication.Response.ToContract
{
    public class ResponseShortContractJson
    {
        public long Id { get; set; }
        public DateTime PickupDateTime { get; set; }
        public DateTime ReturnDueDateTime { get; set; }
        public int TotalDays { get; set; }
        public decimal TotalAmount { get; set; }
        public string ContractStatus { get; set; } = string.Empty;
        
    }
}
