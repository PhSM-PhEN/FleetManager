using Bogus;
using FleetManager.Communication.Request.ToContract;

namespace CommonTestUtilities.Request.ToContract
{
    public class RequestRenewContractJsonBuilder
    {
        public static RequestRenewContractJson Build(long? newRentalPlanId = null, long? mileageContracted = null)
        {
            return new Faker<RequestRenewContractJson>()
                .RuleFor(request => request.NewRentalPlanId, _ => newRentalPlanId)
                .RuleFor(request => request.MileageContracted, _ => mileageContracted)
                .Generate();
        }
    }
}
