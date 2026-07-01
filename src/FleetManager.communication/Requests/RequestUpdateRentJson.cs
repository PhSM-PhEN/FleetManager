namespace FleetManager.Communication.Requests
{
    public class RequestUpdateRentJson
    {
        public long ExtraKm { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
