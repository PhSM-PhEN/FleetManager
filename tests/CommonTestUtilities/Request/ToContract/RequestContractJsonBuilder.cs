using Bogus;
using FleetManager.Communication.Request.ToContract;

namespace CommonTestUtilities.Request.ToContract
{
    public class RequestContractJsonBuilder
    {
        public static RequestContractJson Build(long vehicleId, long tenantId, long rentalPlanId, string rentalType = "Daily")
        {
            var pickupDateTime = DateTime.UtcNow;

            return new Faker<RequestContractJson>()
                .RuleFor(request => request.VehicleId, _ => vehicleId)
                .RuleFor(request => request.TenantId, _ => tenantId)
                .RuleFor(request => request.RentalPlanId, _ => rentalPlanId)
                .RuleFor(request => request.RentalType, _ => rentalType)
                .RuleFor(request => request.MileageContracted, _ => 0) // 0 = usa o valor do plano
                .RuleFor(request => request.TotalAmount, _ => 0)       // 0 = usa o valor do plano
                .RuleFor(request => request.PickupDateTime, _ => pickupDateTime)
                .RuleFor(request => request.ReturnDueDateTime, f => rentalType == "Daily" ? pickupDateTime.AddDays(f.Random.Int(1, 30)) : null)
                .Generate();
        }
    }
}