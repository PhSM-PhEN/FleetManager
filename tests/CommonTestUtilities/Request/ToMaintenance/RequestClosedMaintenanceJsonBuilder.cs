using Bogus;
using FleetManager.Communication.Request.ToMaintenance;

namespace CommonTestUtilities.Request.ToMaintenance
{
    public class RequestClosedMaintenanceJsonBuilder
    {
        public static RequestClosedMaintenanceJson Build(decimal? workshopBudget = null, string? problemDescription = null)
        {
            var faker = new Faker();

            return new Faker<RequestClosedMaintenanceJson>()
                .RuleFor(request => request.WorkshopBudget, _ => workshopBudget ?? faker.Random.Decimal(100, 5000))
                .RuleFor(request => request.ProblemDescription, _ => problemDescription ?? faker.Lorem.Sentence())
                .Generate();
        }
    }
}
