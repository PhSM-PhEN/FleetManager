using Bogus;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Enums;

namespace CommonTestUtilities.Entitie
{
    public class RentalPlanBuilder
    {
        public static RentalPlan Build(TransmissionType? transmissionType = null)
        {
            var faker = new Faker();
            var mode = faker.PickRandom<RentalMode>();
            var transmission = transmissionType ?? faker.PickRandom<TransmissionType>();
            var totalIncludedKm = faker.PickRandom<long>(100, 200);
            var modeName = mode == RentalMode.Daily ? "Daily" : "Monthly";
            var transmissionName = transmission == TransmissionType.Manual ? "Manual" : "Automatic";

            return new RentalPlan(
                name: $"{modeName} {transmissionName}",
                mode: mode,
                transmission: transmission,
                priceRental: faker.Finance.Amount(300, 4000),
                totalKmIncluded: totalIncludedKm,
                pricePerKm: faker.Finance.Amount(1, 10));
        }
    }
}
