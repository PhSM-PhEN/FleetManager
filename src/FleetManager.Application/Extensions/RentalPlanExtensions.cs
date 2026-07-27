using FleetManager.Communication.Response.ToRentalPlan;
using FleetManager.Domain.Entities;

namespace FleetManager.Application.Extensions
{
    public static class RentalPlanExtensions
    {
        public static ResponseRentalPlanJson ToResponse(this RentalPlan rentalPlan)
        {
            return new ResponseRentalPlanJson
            {
                Id = rentalPlan.Id,
                Name = rentalPlan.Name,
                DailyPrice = rentalPlan.DailyPrice,
                MonthlyPrice = rentalPlan.MonthlyPrice,
                ExcessMileageRate = rentalPlan.ExcessMileageRate,
                MileagePerDay = rentalPlan.MileagePerDay,
                MileagePerMonthly = rentalPlan.MileagePerMonthly
            };
        }
        public static List<ResponseRentalPlanJson> ToResponse(this List<RentalPlan> rentalPlan)
        {
            return rentalPlan.Select(p => p.ToResponse()).ToList();
        }
    }
}
