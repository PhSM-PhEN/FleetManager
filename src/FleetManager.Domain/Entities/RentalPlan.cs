using System.Collections.ObjectModel;

namespace FleetManager.Domain.Entities
{
    public class RentalPlan : AudiTableEntity
    {
        public string Name { get; private set; } = string.Empty;
        public decimal DailyPrice { get; private set; }
        public decimal MonthlyPrice { get; private set; }
        public decimal ExcessMileageRate { get; private set; }
        public long MileagePerDay { get; private set; }
        public long MileagePerMonthly { get; private set; }

        public Collection<Vehicle> Vehicles { get; set; } = default!;

        protected RentalPlan() { }

        public RentalPlan(string name, decimal dailyPrice, decimal monthlyPrice, decimal excessMileageRate, long mileagePerDay, long mileagePerMonthly)
        {
            Name = name;
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
