using FleetManager.Communication.Request.ToUser;
using FleetManager.Exception.ExceptionBase;
using FluentValidation;

namespace FleetManager.Application.UseCase.ToUser.Update
{
    public class UpdateValidator : AbstractValidator<RequestUpdateUserJson>
    {
        public UpdateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ResourceErrorMessages.NAME_IS_REQUIRED)
                .MaximumLength(100).WithMessage(ResourceErrorMessages.NAME_CANNOT_EXCEED_100_CHARACTERS);
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(ResourceErrorMessages.EMAIL_IS_REQUIRED)
                .EmailAddress().WithMessage(ResourceErrorMessages.INVALID_EMAIL_FORMAT);

        }

    }
}
