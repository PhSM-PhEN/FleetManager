using Bogus;
using FleetManager.Communication.Requests;

namespace CommonTestUtilities.Request
{
    public class RequestRentJsonBuilder
    {
        public static RequestRentJson Build(long companyId, long clientId, long vehicleId, long rentalPlanId)
        {
            var faker = new Faker();
            var startDate = faker.Date.Future(1);
            var endDate = startDate.AddDays(faker.Random.Int(1, 30));

            return new Faker<RequestRentJson>()
                .RuleFor(r => r.CompanyId, _ => companyId)
                .RuleFor(r => r.ClientId, _ => clientId)
                .RuleFor(r => r.VehicleId, _ => vehicleId)
                .RuleFor(r => r.RentalPlanId, _ => rentalPlanId)
                .RuleFor(r => r.StartDate, _ => startDate)
                .RuleFor(r => r.EndDate, _ => endDate)
                .RuleFor(r => r.ExtraKm, f => f.Random.Long(0, 500))
                .Generate();
        }
    }
}
