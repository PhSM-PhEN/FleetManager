using FleetManager.Communication.Request.ToTenant;
using FleetManager.Exception.ExceptionBase;
using FluentValidation;

namespace FleetManager.Application.UseCase.ToTenant.Update
{
    public class UpdateTenantValidator : AbstractValidator<RequestUpdateTenantJson>
    {
        public UpdateTenantValidator()
        {
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage(ResourceErrorMessages.PHONE_NUMBER_REQUIRED);
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage(ResourceErrorMessages.EMAIL_INVALID)
                .When(x => !string.IsNullOrWhiteSpace(x.Email));

            RuleFor(x => x.AddressId).GreaterThan(0).WithMessage(ResourceErrorMessages.ADDRESS_ID_REQUIRED);
        }
    }
}
