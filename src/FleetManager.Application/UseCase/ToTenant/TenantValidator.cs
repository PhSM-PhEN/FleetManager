using FleetManager.Communication.Request.ToTenant;
using FluentValidation;

namespace FleetManager.Application.UseCase.ToTenant
{
    public class TenantValidator : AbstractValidator<RequestTenantJson>
    {
        public TenantValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("ResourceErrorMessages.NAME_IS_REQUIRED");
            RuleFor(x => x.Cpf).NotEmpty().WithMessage("ResourceErrorMessages.CPF_REQUIRED");
            RuleFor(x => x.Rg).NotEmpty().WithMessage("ResourceErrorMessages.RG_REQUIRED");
            RuleFor(x => x.DriverLicenseNumber).NotEmpty().WithMessage("ResourceErrorMessages.DRIVER_LICENSE_NUMBER_REQUIRED");
            RuleFor(x => x.DriverLicenseCategory).NotEmpty().WithMessage("ResourceErrorMessages.DRIVER_LICENSE_CATEGORY_REQUIRED");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("ResourceErrorMessages.PHONE_NUMBER_REQUIRED");
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("ResourceErrorMessages.EMAIL_INVALID")
                .When(x => !string.IsNullOrWhiteSpace(x.Email));
        
            RuleFor(x => x.AddressId).GreaterThan(0).WithMessage("ResourceErrorMessages.ADDRESS_ID_REQUIRED");
        }
    }
}
