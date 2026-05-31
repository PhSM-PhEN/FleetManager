using FleetManager.Communication.Requests;
using FluentValidation;

namespace FleetManager.Application.UseCase.ToUser.ChangePassword
{
    public class ChangePasswordValidator : AbstractValidator<RequestChangePasswordJson>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.NewPassword).SetValidator(new PasswordValidator<RequestChangePasswordJson>());
        }
    }
}
