using FleetManager.communication.Requests;
using FleetManager.Exception.ExceptionBase;
using FluentValidation;

namespace FleetManager.Application.UseCase.ToRentalPlan
{
    public class RentalPlanValidator : AbstractValidator<RequestRentalPlansJson>
    {

        public RentalPlanValidator()
        {
            RuleFor(rp => rp.Name).NotEmpty().WithMessage(ResourceErrorMessages.NAME_IS_REQUIRED);
            RuleFor(rp => rp.Mode).IsInEnum().WithMessage(ResourceErrorMessages.INVALID_RENTAL_MODE);
            RuleFor(rp => rp.Transmission).IsInEnum().WithMessage(ResourceErrorMessages.INVALID_TRANSMISSION_TYPE);
            RuleFor(rp => rp.PriceRental).GreaterThan(0).WithMessage(ResourceErrorMessages.PRICE_RENTAL_MUST_BE_GREATER_THAN_ZERO);
            RuleFor(rp => rp.PricePerKm).GreaterThan(0).WithMessage(ResourceErrorMessages.PRICE_PER_KM_MUST_BE_GREATER_THAN_ZERO);
             
        }
      
    }
}
