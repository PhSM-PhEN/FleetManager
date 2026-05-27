namespace FleetManager.communication.Requests
{
    public class RequestRentJson
    {
      
        public int CompanyId { get; set; }
        public long ClientId { get; set; }
        public long VehicleId { get; set; }
        public int CategoryId { get; set; }
        public int RentalPlanId {get ; set ;}
        public long UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
