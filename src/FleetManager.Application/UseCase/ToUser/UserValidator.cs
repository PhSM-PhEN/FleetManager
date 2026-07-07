using FleetManager.Communication.Request.ToUser;
using FleetManager.Exception.ExceptionBase;
using FluentValidation;

namespace FleetManager.Application.UseCase.ToUser
{
    public class UserValidator : AbstractValidator<RequestRegisterUserJson>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ResourceErrorMessages.NAME_IS_REQUIRED)
                .MaximumLength(100).WithMessage(ResourceErrorMessages.NAME_CANNOT_EXCEED_100_CHARACTERS);
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(ResourceErrorMessages.EMAIL_IS_REQUIRED)
                .EmailAddress().WithMessage(ResourceErrorMessages.INVALID_EMAIL_FORMAT);
            RuleFor(user => user.Password).SetValidator(new PasswordValidator<RequestRegisterUserJson>());
        }
    }
}
