using FleetManager.Domain.Entities;
using FleetManager.Domain.Enum;

namespace FleetManager.Application.UseCase.ToContract
{
    public static class ContractTermsCalculator
    {
        public static long GetMileageContracted(long excessMileage, RentalType rentalType, RentalPlan rentalPlan, int totalDays)
        {
            return rentalType == RentalType.Daily
                ? (rentalPlan.MileagePerDay + excessMileage) * totalDays
                : rentalPlan.MileagePerMonthly + excessMileage;
        }

        public static decimal GetTotalAmount(long excessMileage, RentalType rentalType, RentalPlan rentalPlan, int totalDays)
        {
            var amount = rentalType == RentalType.Daily
                ? rentalPlan.DailyPrice * totalDays
                : rentalPlan.MonthlyPrice;

            amount += excessMileage * rentalPlan.ExcessMileageRate;

            return amount;
        }
    }
}