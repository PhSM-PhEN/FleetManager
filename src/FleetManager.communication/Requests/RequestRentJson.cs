namespace FleetManager.Communication.Requests
{
    public class RequestRentJson
    {
      
        public int CompanyId { get; set; }
        public long ClientId { get; set; }
        public long VehicleId { get; set; }
        public int RentalPlanId {get ; set ;}
        public decimal IncludedKm { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
