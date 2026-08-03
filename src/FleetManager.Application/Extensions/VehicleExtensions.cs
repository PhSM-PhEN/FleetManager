using FleetManager.Communication.Response.ToVehicle;
using FleetManager.Domain.Entities;

namespace FleetManager.Application.Extensions
{
    public static class VehicleExtensions
    {
        public static ResponseShortVehicleJson ToResponse(this Vehicle vehicle)
        {
            return new ResponseShortVehicleJson
            {
                Id = vehicle.Id,
                Brand = vehicle.Brand,
                Model = vehicle.Model,
                Color = vehicle.Color,
                ManufacturingYear = vehicle.ManufacturerYear.ToString(),
                ChassiNumber = vehicle.ChassiNumber.Number,
                LicensePlate = vehicle.LicensePlate.Number,
                CurrentMileage = vehicle.CurrentMileage,
                Company = vehicle.Company.ToResponse()
            };
        }
        public static ResponseVehicleJson ToInfoResponse(this Vehicle vehicle)
        {
            return new ResponseVehicleJson
            {
                Id = vehicle.Id,
                Brand = vehicle.Brand,
                Model = vehicle.Model,
                Color = vehicle.Color,
                ManufacturingYear = vehicle.ManufacturerYear.ToString(),
                Renavam = vehicle.Renavam.Number,
                ChassiNumber = vehicle.ChassiNumber.Number,
                LicensePlate = vehicle.LicensePlate.Number,
                CurrentMileage = vehicle.CurrentMileage,
                Company = vehicle.Company.ToResponse()
            };
        }
        public static List<ResponseShortVehicleJson> ToResponse(this List<Vehicle> vehicles)
        {
            return vehicles.Select(v => v.ToResponse()).ToList();
        }
    }
}
