using FleetManager.Communication.Response;
using FleetManager.Communication.Response.ToVehicle;
using FleetManager.Domain.Entities;
using FleetManager.Domain.EnumExtensions;

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
                ManufacturingYear = vehicle.ManufacturingYear.ToString(),
                ChassiNumber = vehicle.ChassiNumber.Number,
                LicensePlate = vehicle.LicensePlate.Number,
                CurrentMileage = vehicle.CurrentMileage,
                Company = vehicle.Company.ToResponse(),
                Status = new ResponseEnumStatusJson
                {
                    Id = (int)vehicle.Status,
                    Label = vehicle.Status.ToStringStatus()
                }
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
                ManufacturingYear = vehicle.ManufacturingYear.ToString(),
                Renavam = vehicle.Renavam.Number,
                ChassiNumber = vehicle.ChassiNumber.Number,
                LicensePlate = vehicle.LicensePlate.Number,
                CurrentMileage = vehicle.CurrentMileage,
                Company = vehicle.Company.ToResponse(),
                RentalPlan = vehicle.RentalPlan.ToResponse(),
                Status = new ResponseEnumStatusJson
                {
                    Id = (int)vehicle.Status,
                    Label = vehicle.Status.ToStringStatus()
                }
                
            
            };
        }
        public static List<ResponseShortVehicleJson> ToResponse(this List<Vehicle> vehicles)
        {
            return vehicles.Select(v => v.ToResponse()).ToList();
        }

        public static ResponseRegisterVehicleJson ToShortResponse(this Vehicle vehicle)
        {
            return new ResponseRegisterVehicleJson
            {
                Id = vehicle.Id,
                LicensePlate = vehicle.LicensePlate.Number,
                Model = vehicle.Model,
                CurrentMileage = vehicle.CurrentMileage,
                Status = new ResponseEnumStatusJson
                {
                    Id = (int)vehicle.Status,
                    Label = vehicle.Status.ToStringStatus()
                }
            };
        }
    }
}
