using FleetManager.Communication.Request.ToContract;
using FleetManager.Exception.ExceptionBase;
using FluentValidation;

namespace FleetManager.Application.UseCase.ToContract.Renew
{
    public class RenewContractValidator : AbstractValidator<RequestRenewContractJson>
    {
        public RenewContractValidator()
        {
            RuleFor(x => x.MileageContracted)
                .GreaterThan(0)
                .When(x => x.MileageContracted.HasValue)
                .WithMessage(ResourceErrorMessages.MILEAGE_CONTRACTED_INVALID);

            RuleFor(x => x.NewRentalPlanId)
                .GreaterThan(0)
                .When(x => x.NewRentalPlanId.HasValue)
                .WithMessage(ResourceErrorMessages.RENTAL_PLAN_ID_REQUIRED);
        }
    }
}