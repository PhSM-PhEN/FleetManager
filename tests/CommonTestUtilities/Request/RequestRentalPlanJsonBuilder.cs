using Bogus;
using FleetManager.Communication.Requests;
using FleetManager.Communication.ToEnums;

namespace CommonTestUtilities.Request;

public class RequestRentalPlanJsonBuilder
{
    public static RequestRentalPlansJson Build()
    {
        return new Faker<RequestRentalPlansJson>()
            .RuleFor(r => r.Name, f => f.PickRandom("Daly manual", "Monthly manual", "Daly automatic", "monthly automatic"))
            .RuleFor(r => r.Mode, f => f.PickRandom<RentalMode>())
            .RuleFor(r => r.Transmission, f => f.PickRandom<TransmissionType>())
            .RuleFor(r => r.PricePerKm, f => f.Finance.Amount(1, 10))
            .RuleFor(r => r.PricePerKm, f => f.Finance.Amount(300, 4000))
            .Generate();

    }

}
