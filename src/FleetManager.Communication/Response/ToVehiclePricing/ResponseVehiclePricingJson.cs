namespace FleetManager.Communication.Response.ToVehiclePricing
{
    public class ResponseVehiclePricingJson
    {
        public long Id { get; set; }
        public long VehicleId { get; set; }
        public decimal DailyPrice { get; set; }
        public decimal MonthlyPrice { get; set; }
        public decimal ExcessMileageRate { get; set; }
        public long MileagePerDay { get; set; }
        public long MileagePerMonthly { get; set; }
    }
}
