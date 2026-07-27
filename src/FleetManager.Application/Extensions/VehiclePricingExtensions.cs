using FleetManager.Communication.Response.ToVehiclePricing;
using FleetManager.Domain.Entities;

namespace FleetManager.Application.Extensions
{
    public static class VehiclePricingExtensions
    {
        public static ResponseVehiclePricingJson ToResponse(this VehiclePricing pricing)
        {
            return new ResponseVehiclePricingJson
            {
                Id = pricing.Id,
                Name = pricing.Name,
                DailyPrice = pricing.DailyPrice,
                MonthlyPrice = pricing.MonthlyPrice,
                ExcessMileageRate = pricing.ExcessMileageRate,
                MileagePerDay = pricing.MileagePerDay,
                MileagePerMonthly = pricing.MileagePerMonthly
            };
        }
        public static List<ResponseVehiclePricingJson> ToResponse(this List<VehiclePricing> pricing)
        {
            return pricing.Select(p => p.ToResponse()).ToList();
        }
    }
}
