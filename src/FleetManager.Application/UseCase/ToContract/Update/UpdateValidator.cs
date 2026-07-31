using FleetManager.Communication.Request.ToContract;
using FleetManager.Exception.ExceptionBase;
using FluentValidation;

namespace FleetManager.Application.UseCase.ToContract.Update
{
    public class UpdateValidator : AbstractValidator<RequestUpdateContractJson>
    {
        public UpdateValidator()
        {
            RuleFor(x => x.MileageContracted).GreaterThanOrEqualTo(0).WithMessage(ResourceErrorMessages.MILEAGE_CONTRACTED_INVALID);
            RuleFor(x => x.TotalAmount).GreaterThanOrEqualTo(0).WithMessage(ResourceErrorMessages.TOTAL_AMOUNT_INVALID);

            RuleFor(x => x.PickupDateTime).NotNull()
            .WithMessage("f");

            RuleFor(x => x.ReturnDueDateTime)
            .NotNull().WithMessage(ResourceErrorMessages.RETURN_DUE_DATE_REQUIRED);
            

        }
    }
}
