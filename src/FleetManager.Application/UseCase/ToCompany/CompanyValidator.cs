using FleetManager.Communication.Request.ToCompany;
using FleetManager.Exception.ExceptionBase;
using FluentValidation;

namespace FleetManager.Application.UseCase.ToCompany;

public class CompanyValidator : AbstractValidator<RequestCompanyJson>
{
    public CompanyValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage(ResourceErrorMessages.NAME_IS_REQUIRED);

        RuleFor(x => x.Cnpj).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(ResourceErrorMessages.CNPJ_REQUIRED)
            .Matches(@"^\d{2}\.?\d{3}\.?\d{3}\/?\d{4}-?\d{2}$")
            .WithMessage(ResourceErrorMessages.CNPJ_INVALID);

        RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage(ResourceErrorMessages.PHONE_NUMBER_REQUIRED);

        RuleFor(x => x.AddressId).GreaterThan(0).WithMessage(ResourceErrorMessages.ADDRESS_ID_REQUIRED);
    }
}