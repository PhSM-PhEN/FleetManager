using FleetManager.communication.Requests.ToUser;
using FleetManager.Exception.ExceptionBase;
using FluentValidation;

namespace FleetManager.Application.UseCase.ToUser.Update
{
    public class UpdateValidator : AbstractValidator<RequestUpdateUserJson>
    {
        public UpdateValidator()
        {
            RuleFor(user => user.Name)
                .NotEmpty().WithMessage(ResourceErrorMessages.NAME_REQUIRED);
            RuleFor(user => user.Email)
                .NotEmpty().WithMessage(ResourceErrorMessages.EMAIL_REQUIRED)
                .EmailAddress()
                .When(user => string.IsNullOrWhiteSpace(user.Email) == false, applyConditionTo: ApplyConditionTo.CurrentValidator)
                .WithMessage(ResourceErrorMessages.EMAIL_INVALID);



        }
    }
}
