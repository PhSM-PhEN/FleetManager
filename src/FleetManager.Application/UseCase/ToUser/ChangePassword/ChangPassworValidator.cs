using FleetManager.Communication.Request.ToUser;
using FluentValidation;

namespace FleetManager.Application.UseCase.ToUser.ChangePassword
{
    public class ChangPassworValidator : AbstractValidator<RequestChangPasswordJson>
    {

        public ChangPassworValidator()
        {
            RuleFor(x => x.NewPassword).SetValidator(new PasswordValidator<RequestChangPasswordJson>());
        }
    }
}
