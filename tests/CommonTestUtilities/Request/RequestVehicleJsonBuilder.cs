using System;
using Bogus;
using FleetManager.Communication.Requests;

namespace CommonTestUtilities.Request;

public class RequestVehicleJsonBuilder
{
    public static RequestVehicleJson Build(int categoryId)
    {
        return new Faker<RequestVehicleJson>()

        .RuleFor(r => r.Brand, f => f.Vehicle.Manufacturer())
        .RuleFor(r => r.Model, f => f.Vehicle.Model())
        .RuleFor(r => r.ManufacturingYear, f => f.Date.Past(8).Year)
        .RuleFor(r => r.Renavam, f => f.Random.Replace("###########"))
        .RuleFor(r => r.ChassisNumber, f => f.Vehicle.Vin())
        .RuleFor(r => r.LicensePlate, f => f.Random.Replace("???-####"))
        .RuleFor(r => r.Color, f => f.Commerce.Color())
        .RuleFor(r => r.CurrentMileage, f => f.Random.Decimal(0, 30000))
        .RuleFor(r => r.CategoryId, _ = categoryId)
        .Generate();
    }
}
