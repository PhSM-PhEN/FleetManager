using Bogus;
using FleetManager.Domain.Entities;

namespace CommonTestUtilities.Entities
{
    public class VehiclePricingBuilder
    {
        public static VehiclePricing Build(int? id = null, long? vehicleId = null)
        {
            var pricing = new Faker<VehiclePricing>()
                .CustomInstantiator(f => new VehiclePricing(
                    vehicleId ?? f.Random.Long(1, 1000),
                    f.Random.Decimal(100, 400),
                    f.Random.Decimal(2500, 6000),
                    f.Random.Decimal(0.5m, 2m),
                    f.Random.Long(100, 300),
                    f.Random.Long(3000, 5000)
                ))
                .Generate();

            if (id.HasValue)
                pricing.Id = id.Value;

            return pricing;
        }
    }
}
