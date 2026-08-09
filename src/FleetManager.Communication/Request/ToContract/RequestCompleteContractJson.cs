namespace FleetManager.Communication.Request.ToContract
{
    public class RequestCompleteContractJson
    {
        public DateTime? ActualReturnDateTime { get; set; }
        public long FinalMileage { get; set; }
    }
}
