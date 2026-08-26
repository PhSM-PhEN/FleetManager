namespace FleetManager.Communication.Response.ToContract
{
    public class ResponseFinishUpContractJson
    {
        public long ContractId { get; set; }
        public DateTime ActualReturnDateTime { get; set; }
        public long FinalMileage { get; set; }
        public decimal? ExcessMileageFee { get; set; }
        public int DaysLate { get; set; }
        public decimal? LateFee { get; set; }
        public decimal TotalCharged { get; set; }
        public ResponseEnumStatusJson Status { get; set; } = new();
    }
}
