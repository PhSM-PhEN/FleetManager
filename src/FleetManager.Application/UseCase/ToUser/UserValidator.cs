using FleetManager.communication.Requests.ToUser;
using FleetManager.Exception.ExceptionBase;
using FluentValidation;

namespace FleetManager.Application.UseCase.ToUser
{
    public class UserValidator : AbstractValidator<RequestRegisterUserJson>
    {
        public UserValidator()
        {
            RuleFor(u => u.Name)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.NAME_REQUIRED);
            RuleFor(u => u.Email)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.EMAIL_REQUIRED)
                .EmailAddress()
                .When(user => !string.IsNullOrEmpty(user.Email) == false, ApplyConditionTo.CurrentValidator)
                .WithMessage(ResourceErrorMessages.EMAIL_INVALID);

            RuleFor(user  => user.Password).SetValidator(new PasswordValidator<RequestRegisterUserJson>());

        }
    }
}
