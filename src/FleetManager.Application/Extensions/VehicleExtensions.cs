using System.Security.Cryptography.X509Certificates;
using FleetManager.Communication.Responses;
using FleetManager.Domain.Entities;

namespace FleetManager.Application.Extensions
{
    public static class VehicleExtensions
    {
        public static ResponseVehicleJson ToShortResponse(this Vehicle vehicle)
        {
            return new ResponseVehicleJson
            {
                Id = vehicle.Id,
                LicensePlate = vehicle.LicensePlate,
                Brand = vehicle.Brand,
                CurrentMileage = vehicle.CurrentMileage
            };
        }
        public static ResponseRegisterVehicleJson ToRegisterResponse(this Vehicle vehicle)
        {
            return new ResponseRegisterVehicleJson
            {
                Id = vehicle.Id,
                LicensePlate = vehicle.LicensePlate,
                Brand = vehicle.Brand,
                CurrentMileage = vehicle.CurrentMileage,
                CategoryId = vehicle.CategoryId

            };
        }
        public static ResponseVehicleByIdJson ToDetailResponse(this Vehicle vehicle)
        {
            return new ResponseVehicleByIdJson
            {
                Id = vehicle.Id,
                Brand = vehicle.Brand,
                Model = vehicle.Model,
                ManufacturingYear = vehicle.ManufacturingYear,
                Renavam = vehicle.Renavam,
                LicensePlate = vehicle.LicensePlate,
                Color = vehicle.Color,
                CurrentMileage = vehicle.CurrentMileage,
                CreateAt = vehicle.CreateAt,
                CategoryId = vehicle.CategoryId,
                Category = vehicle.Category.ToResponse()  
            };
        }
        public static List<ResponseVehicleJson> ToShortResponse(this List<Vehicle> vehicles)
        {
           return vehicles.Select(v => v.ToShortResponse()).ToList();
        }
    }
}

