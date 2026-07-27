using Bogus;
using FleetManager.Communication.Request.ToVehiclePricing;

namespace CommonTestUtilities.Request.ToVehiclePricing
{
    public class RequestVehiclePricingJsonBuilder
    {
        public static RequestVehiclePricingJson Build()
        {
            return new Faker<RequestVehiclePricingJson>()
                .RuleFor(request => request.Name, f => f.PickRandom("suv", "hacth", "sedan") )
                .RuleFor(request => request.DailyPrice, f => f.Random.Decimal(100, 400))
                .RuleFor(request => request.MonthlyPrice, f => f.Random.Decimal(2500, 6000))
                .RuleFor(request => request.ExcessMileageRate, f => f.Random.Decimal(0.5m, 2m))
                .RuleFor(request => request.MileagePerDay, f => f.Random.Long(100, 300))
                .RuleFor(request => request.MileagePerMonthly, f => f.Random.Long(3000, 5000))
                .Generate();
        }
    }
}
