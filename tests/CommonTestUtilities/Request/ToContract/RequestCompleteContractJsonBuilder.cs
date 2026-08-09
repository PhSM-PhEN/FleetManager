using Bogus;
using FleetManager.Communication.Request.ToContract;

namespace CommonTestUtilities.Request.ToContract
{
    public class RequestCompleteContractJsonBuilder
    {
        public static RequestCompleteContractJson Build(long finalMileage)
        {
            return new Faker<RequestCompleteContractJson>()
                .RuleFor(request => request.ActualReturnDateTime, _ => DateTime.UtcNow)
                .RuleFor(request => request.FinalMileage, _ => finalMileage)
                .Generate();
        }
    }
}
