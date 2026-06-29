namespace FleetManager.Communication.Requests
{
    public class RequestUpdateRentJson
    {
        public long IncludedKm { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
