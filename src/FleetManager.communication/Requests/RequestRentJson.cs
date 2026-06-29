namespace FleetManager.Communication.Requests
{
    public class RequestRentJson
    {
      
        public long CompanyId { get; set; }
        public long ClientId { get; set; }
        public long VehicleId { get; set; }
        public long RentalPlanId {get ; set ;}
        public long IncludedKm { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
