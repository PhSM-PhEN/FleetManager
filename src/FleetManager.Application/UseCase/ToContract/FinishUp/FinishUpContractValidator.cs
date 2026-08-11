using FleetManager.Communication.Request.ToContract;
using FleetManager.Exception.ExceptionBase;
using FluentValidation;

namespace FleetManager.Application.UseCase.ToContract.FinishUp
{
    public class FinishUpContractValidator : AbstractValidator<RequestFinishUpContractJson>
    {
        public FinishUpContractValidator()
        {
            RuleFor(x => x.FinalMileage).GreaterThanOrEqualTo(0).WithMessage(ResourceErrorMessages.MILEAGE_INVALID);
        }
    }
}
