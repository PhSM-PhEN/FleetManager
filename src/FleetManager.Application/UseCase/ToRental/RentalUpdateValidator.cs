using FleetManager.Communication.Requests;
using FleetManager.Exception.ExceptionBase;
using FluentValidation;

namespace FleetManager.Application.UseCase.ToRental
{
    public class RentalUpdateValidator : AbstractValidator<RequestUpdateRentJson>
    {
        public RentalUpdateValidator()
        {


            RuleFor(rent => rent.StartDate)
                .LessThan(rent => rent.EndDate)
                .WithMessage(ResourceErrorMessages.START_DATE_MUST_BE_LESS_THAN_END_DATE);

            RuleFor(rent => rent.EndDate)
                .GreaterThan(DateTime.UtcNow)
                .WithMessage(ResourceErrorMessages.END_DATE_MUST_BE_GREATER_THAN_CURRENT_DATE);
        }
    }
}

