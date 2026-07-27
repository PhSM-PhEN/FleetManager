namespace FleetManager.Domain.Entities
{
    public class VehiclePricing : AudiTableEntity
    {
        public long VehicleId { get; set; }
        public decimal DalyPrice { get; private set; }
        public decimal MonthlyPrice { get; private set; }
        public decimal ExcessMileageRate { get; private set; }
        public long MileagePerDay { get; private set; }
        public long MileagePerMonthly { get; private set; }

        public Vehicle Vehicle { get; set; } = default!;

        protected VehiclePricing() { }

        public VehiclePricing(long vehicleId, decimal dalyPrice, decimal monthlyPrice, decimal excessMileageRate, long mileagePerDay, long mileagePerMonthly)
        {
            VehicleId = vehicleId;
            DalyPrice = dalyPrice;
            MonthlyPrice = monthlyPrice;
            ExcessMileageRate = excessMileageRate;
            MileagePerDay = mileagePerDay;
            MileagePerMonthly = mileagePerMonthly;
        }
        public void Update(long vehicleId, decimal dalyPrice, decimal monthlyPrice, decimal excessMileageRate, long mileagePerDay, long mileagePerMonthly)
        {
            VehicleId = vehicleId;
            DalyPrice = dalyPrice;
            MonthlyPrice = monthlyPrice;
            ExcessMileageRate = excessMileageRate;
            MileagePerDay = mileagePerDay;
            MileagePerMonthly = mileagePerMonthly;
        }
    }
}
