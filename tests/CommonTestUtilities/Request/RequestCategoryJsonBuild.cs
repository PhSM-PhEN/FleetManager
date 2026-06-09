using Bogus;
using FleetManager.Communication.Requests;
using FleetManager.Communication.ToEnums;

namespace CommonTestUtilities.Request;

public class RequestCategoryJsonBuild
{
    public static RequestCategoryJson Build()
    {
        return new Faker<RequestCategoryJson>()
            .RuleFor(r => r.Name,             f => f.PickRandom("SUV", "Hatch", "Sedan", "Pickup", "Minivan"))
            .RuleFor(r => r.TransmissionType, f => f.PickRandom<TransmissionType>())
            .Generate();
    }
}
