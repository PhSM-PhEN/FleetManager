namespace FleetManager.Communication.Requests
{
    public class RequestUpdateRentJson
    {
        public decimal IncludedKm { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
