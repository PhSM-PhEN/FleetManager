using FleetManager.Domain.Entities;
using FleetManager.Domain.Enum;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToContract
{
    /// <summary>
    /// Regra única de "km contratada padrão" e "valor total padrão" a partir do plano de locação,
    /// usada tanto no registro do contrato quanto na prévia (preview) — evita que as duas rotas
    /// divirjam silenciosamente no futuro.
    /// </summary>
    public static class ContractTermsCalculator
    {
        public static long GetMileageContracted(long mileageContracted, RentalType rentalType, RentalPlan rentalPlan, int totalDays)
        {
            if (mileageContracted > 0)
                return mileageContracted;

            return rentalType == RentalType.Daily
                ? rentalPlan.MileagePerDay * totalDays
                : rentalPlan.MileagePerMonthly;
        }

        public static decimal GetTotalAmount(decimal totalAmount, RentalType rentalType, RentalPlan rentalPlan, int totalDays)
        {
           
            var amount =  rentalType == RentalType.Daily
                ? rentalPlan.DailyPrice * totalDays
                : rentalPlan.MonthlyPrice;
            if(totalAmount > 0 && totalAmount < ( amount / 2))
            {
                throw new BusinessRuleException("Valor de desconto excede o permitido");
            }
            else
            {
                amount = totalAmount;
            }

            return amount;
        }
    }
}
