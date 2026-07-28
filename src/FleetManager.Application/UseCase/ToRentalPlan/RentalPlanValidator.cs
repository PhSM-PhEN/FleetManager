using FleetManager.Communication.Request.ToRentalPlan;
using FleetManager.Exception.ExceptionBase;
using FluentValidation;

namespace FleetManager.Application.UseCase.ToRentalPlan
{
    public class RentalPlanValidator : AbstractValidator<RequestRentalPlanJson>
    {
        public RentalPlanValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(ResourceErrorMessages.NAME_IS_REQUIRED);
            RuleFor(x => x.DailyPrice).GreaterThan(0).WithMessage(ResourceErrorMessages.DAILY_PRICE_INVALID);
            RuleFor(x => x.MonthlyPrice).GreaterThan(0).WithMessage(ResourceErrorMessages.MONTHLY_PRICE_INVALID);
            RuleFor(x => x.ExcessMileageRate).GreaterThanOrEqualTo(0).WithMessage(ResourceErrorMessages.EXCESS_MILEAGE_RATE_INVALID);
            RuleFor(x => x.MileagePerDay).GreaterThan(0).WithMessage(ResourceErrorMessages.MILEAGE_PER_DAY_INVALID);
            RuleFor(x => x.MileagePerMonthly).GreaterThan(0).WithMessage(ResourceErrorMessages.MILEAGE_PER_MONTHLY_INVALID);
        }
    }
}
