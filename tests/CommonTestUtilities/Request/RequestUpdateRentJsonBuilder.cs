using Bogus;
using FleetManager.Communication.Requests;

namespace CommonTestUtilities.Request
{
    public class RequestUpdateRentJsonBuilder
    {
        public static RequestUpdateRentJson Build()
        {
            var faker = new Faker();
            var startDate = faker.Date.Future(1);
            var endDate = startDate.AddDays(faker.Random.Int(1, 30));

            return new Faker<RequestUpdateRentJson>()
                .RuleFor(r => r.StartDate, _ => startDate)
                .RuleFor(r => r.EndDate, _ => endDate)
                .RuleFor(r => r.ExtraKm, f => f.Random.Long(0, 500))
                .Generate();
        }
    }
}
