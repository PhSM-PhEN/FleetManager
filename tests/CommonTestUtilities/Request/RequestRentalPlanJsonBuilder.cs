using Bogus;
using FleetManager.Communication.Requests;
using FleetManager.Communication.ToEnums;

namespace CommonTestUtilities.Request;

public class RequestRentalPlanJsonBuilder
{
    public static RequestRentalPlansJson Build()
    {
        var faker = new Faker();
        var mode = faker.PickRandom<RentalMode>();
        var transmission = faker.PickRandom<TransmissionType>();

        var modeName = mode == RentalMode.Daily ? "Daly" : "Monthly";
        var transmissionName = transmission == TransmissionType.Manual ? "manual" : "automatic";

        return new Faker<RequestRentalPlansJson>()
            .RuleFor(r => r.Name, _ => $"{modeName} {transmissionName}")
            .RuleFor(r => r.Mode, _ => mode)
            .RuleFor(r => r.Transmission, _ => transmission)
            .RuleFor(r => r.PriceRental, f => f.Finance.Amount(300, 4000))
            .RuleFor(r => r.PricePerKm, f => f.Finance.Amount(1, 10))
            .Generate();

    }

}
