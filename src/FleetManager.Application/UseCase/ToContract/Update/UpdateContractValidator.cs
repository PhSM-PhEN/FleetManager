using FleetManager.Communication.Request.ToContract;
using FleetManager.Exception.ExceptionBase;
using FluentValidation;

namespace FleetManager.Application.UseCase.ToContract.Update
{
    public class UpdateContractValidator : AbstractValidator<RequestUpdateContractJson>
    {
        public UpdateContractValidator()
        {
            RuleFor(x => x.MileageContracted).GreaterThanOrEqualTo(0).WithMessage(ResourceErrorMessages.MILEAGE_CONTRACTED_INVALID);
            RuleFor(x => x.TotalAmount).GreaterThanOrEqualTo(0).WithMessage(ResourceErrorMessages.TOTAL_AMOUNT_INVALID);

            RuleFor(x => x.RentalType)
                .Must(value => value == "Daily" || value == "Monthly")
                .WithMessage(ResourceErrorMessages.RENTAL_TYPE_INVALID);

            RuleFor(x => x.ReturnDueDateTime)
                .NotNull().WithMessage(ResourceErrorMessages.RETURN_DUE_DATE_REQUIRED)
                .When(x => x.RentalType == "Daily");

            RuleFor(x => x.ReturnDueDateTime)
                .Must((request, returnDue) => returnDue!.Value > request.PickupDateTime)
                .WithMessage(ResourceErrorMessages.RETURN_DUE_DATE_MUST_BE_AFTER_PICKUP)
                .When(x => x.RentalType == "Daily" && x.ReturnDueDateTime.HasValue);
        }
    }
}
