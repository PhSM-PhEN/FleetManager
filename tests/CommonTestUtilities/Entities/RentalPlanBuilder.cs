using Bogus;
using FleetManager.Domain.Entities;

namespace CommonTestUtilities.Entities
{
    public class RentalPlanBuilder
    {
        public static List<RentalPlan> Collection(uint count = 3)
        {
            var list = new List<RentalPlan>();
            if (count == 0)
                count = 1;
            var rentalPlanId = 1;

            for (var i = 0; i < count; i++)
            {
                var rentalPlan = Build();
                rentalPlan.Id = rentalPlanId++;

                list.Add(rentalPlan);
            }

            return list;
        }

        public static RentalPlan Build(long? id = null)
        {
            var rentalPlan = new Faker<RentalPlan>()
                .CustomInstantiator(f => new RentalPlan(
                    f.PickRandom("suv","hacth", "sedan"),
                    f.Random.Decimal(100, 400),
                    f.Random.Decimal(2500, 6000),
                    f.Random.Decimal(0.5m, 2m),
                    f.Random.Long(100, 300),
                    f.Random.Long(3000, 5000)
                ))
                .Generate();

            if (id.HasValue)
                rentalPlan.Id = id.Value;

            return rentalPlan;
        }
    }
}
