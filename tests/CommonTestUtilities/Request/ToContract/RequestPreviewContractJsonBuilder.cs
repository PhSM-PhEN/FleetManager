using Bogus;
using FleetManager.Communication.Request.ToContract;

namespace CommonTestUtilities.Request.ToContract
{
    public class RequestPreviewContractJsonBuilder
    {
        public static RequestPreviewContractJson Build(long vehicleId, long tenantId, string rentalType = "Daily")
        {
            var pickupDateTime = DateTime.UtcNow;

            return new Faker<RequestPreviewContractJson>()
                .RuleFor(request => request.VehicleId, _ => vehicleId)
                .RuleFor(request => request.TenantId, _ => tenantId)
                .RuleFor(request => request.RentalType, _ => rentalType)
                .RuleFor(request => request.PickupDateTime, _ => pickupDateTime)
                .RuleFor(request => request.ReturnDueDateTime, f => rentalType == "Daily" ? pickupDateTime.AddDays(f.Random.Int(1, 30)) : null)
                .Generate();
        }
    }
}
