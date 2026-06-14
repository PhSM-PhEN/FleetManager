using Bogus;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Enums;

namespace CommonTestUtilities.Entitie
{
    public class RentalPlanBuilder
    {
        public static RentalPlan Build()
        {
            var faker = new Faker();
            var mode = faker.PickRandom<RentalMode>();
            var transmission = faker.PickRandom<TransmissionType>();

            var modeName = mode == RentalMode.Daily ? "Daily" : "Monthly";
            var transmissionName = transmission == TransmissionType.Manual ? "Manual" : "Automatic";

            return new RentalPlan(
                $"{modeName} {transmissionName}",
                mode,
                transmission,
                faker.Finance.Amount(300, 4000),
                faker.Finance.Amount(1, 10));
        }
    }
}
