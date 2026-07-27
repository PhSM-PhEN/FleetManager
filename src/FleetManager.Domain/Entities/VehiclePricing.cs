namespace FleetManager.Domain.Entities
{
    public class VehiclePricing : AudiTableEntity
    {
        public long VehicleId { get; private set; }
        public decimal DailyPrice { get; private set; }
        public decimal MonthlyPrice { get; private set; }
        public decimal ExcessMileageRate { get; private set; }
        public long MileagePerDay { get; private set; }
        public long MileagePerMonthly { get; private set; }

        public Vehicle Vehicle { get; set; } = default!;

        protected VehiclePricing() { }

        public VehiclePricing(long vehicleId, decimal dailyPrice, decimal monthlyPrice, decimal excessMileageRate, long mileagePerDay, long mileagePerMonthly)
        {
            VehicleId = vehicleId;
            DailyPrice = dailyPrice;
            MonthlyPrice = monthlyPrice;
            ExcessMileageRate = excessMileageRate;
            MileagePerDay = mileagePerDay;
            MileagePerMonthly = mileagePerMonthly;
        }
        public void Update(decimal dailyPrice, decimal monthlyPrice, decimal excessMileageRate, long mileagePerDay, long mileagePerMonthly)
        {
            DailyPrice = dailyPrice;
            MonthlyPrice = monthlyPrice;
            ExcessMileageRate = excessMileageRate;
            MileagePerDay = mileagePerDay;
            MileagePerMonthly = mileagePerMonthly;
        }
    }
}
