using System;
using Bogus;
using FleetManager.Communication.Requests;

namespace CommonTestUtilities.Request;

public class RequestVehicleUpdateCurrentMileageJsonBuilder
{
    public static RequestVehicleUpdateCurrentMileageJson Build()
    {
        return new Faker<RequestVehicleUpdateCurrentMileageJson>()
            .RuleFor(r => r.CurrentMileage, f => f.Random.Decimal(1000, 40000))
            .Generate();
    }
}
