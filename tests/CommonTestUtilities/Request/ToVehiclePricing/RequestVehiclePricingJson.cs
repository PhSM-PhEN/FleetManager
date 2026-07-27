using Bogus;
using FleetManager.Communication.Request.ToRentalPlan;

namespace CommonTestUtilities.Request.ToVehiclePricing
{
    public class RequestRentalPlanJsonBuilder
    {
        public static RequestRentalPlanJson Build()
        {
            return new Faker<RequestRentalPlanJson>()
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
