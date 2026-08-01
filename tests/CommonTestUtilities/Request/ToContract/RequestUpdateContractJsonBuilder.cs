using Bogus;
using FleetManager.Communication.Request.ToContract;

namespace CommonTestUtilities.Request.ToContract
{
    public class RequestUpdateContractJsonBuilder
    {
        public static RequestUpdateContractJson Build(string rentalType = "Daily")
        {
            var pickupDateTime = DateTime.UtcNow;

            return new Faker<RequestUpdateContractJson>()
                .RuleFor(request => request.RentalType, _ => rentalType)
                .RuleFor(request => request.MileageContracted, _ => 0) // 0 = usa o valor do plano
                .RuleFor(request => request.TotalAmount, _ => 0)       // 0 = usa o valor do plano
                .RuleFor(request => request.PickupDateTime, _ => pickupDateTime)
                .RuleFor(request => request.ReturnDueDateTime, f => rentalType == "Daily" ? pickupDateTime.AddDays(f.Random.Int(1, 30)) : null)
                .Generate();
        }
    }
}
