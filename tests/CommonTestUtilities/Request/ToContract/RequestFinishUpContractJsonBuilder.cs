using Bogus;
using FleetManager.Communication.Request.ToContract;

namespace CommonTestUtilities.Request.ToContract
{
    public class RequestFinishUpContractJsonBuilder
    {
        public static RequestFinishUpContractJson Build(long finalMileage)
        {
            return new Faker<RequestFinishUpContractJson>()
                
                .RuleFor(request => request.FinalMileage, _ => finalMileage)
                .Generate();
        }
    }
}
