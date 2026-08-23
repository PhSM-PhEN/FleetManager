using FleetManager.Domain.Entities;
using FleetManager.Domain.Enum;
using FleetManager.Exception.ExceptionBase;

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

        public static long DeriveExcessMileage(long mileageContracted, RentalType rentalType, RentalPlan rentalPlan, int totalDays)
        {
            return rentalType == RentalType.Daily
                ? (mileageContracted / totalDays) - rentalPlan.MileagePerDay
                : mileageContracted - rentalPlan.MileagePerMonthly;
        }

        public static void ValidateTotalAmount(decimal totalAmount, decimal referenceAmount)
        {
            if (totalAmount < referenceAmount / 2)
                throw new BusinessRuleException(ResourceErrorMessages.TOTAL_AMOUNT_DISCOUNT_EXCEEDS_LIMIT);
        }
    }
}