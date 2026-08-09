using FleetManager.Communication.Request.ToContract;
using FleetManager.Exception.ExceptionBase;
using FluentValidation;

namespace FleetManager.Application.UseCase.ToContract.Complete
{
    public class CompleteContractValidator : AbstractValidator<RequestCompleteContractJson>
    {
        public CompleteContractValidator()
        {
            RuleFor(x => x.FinalMileage).GreaterThanOrEqualTo(0).WithMessage(ResourceErrorMessages.MILEAGE_INVALID);
        }
    }
}
