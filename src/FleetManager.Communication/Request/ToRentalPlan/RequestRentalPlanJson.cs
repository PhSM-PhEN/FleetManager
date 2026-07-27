namespace FleetManager.Communication.Request.ToRentalPlan
{
    public class RequestRentalPlanJson
    {
        public string Name {get ; set ;} = string.Empty;
        public decimal DailyPrice { get; set; }
        public decimal MonthlyPrice { get; set; }
        public decimal ExcessMileageRate { get; set; }
        public long MileagePerDay { get; set; }
        public long MileagePerMonthly { get; set; }
    }
}
