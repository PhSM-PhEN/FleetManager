using FleetManager.Communication.Requests;
using FleetManager.Exception.ExceptionBase;
using FluentValidation;

namespace FleetManager.Application.UseCase.ToCompany;

public class CompanyValidator : AbstractValidator<RequestCompanyJson>
{
    public CompanyValidator()
    {
        RuleFor(c => c.Name).NotEmpty()
            .WithMessage(ResourceErrorMessages.NAME_IS_REQUIRED);
        RuleFor(c => c.Cnpj).NotEmpty()
            .WithMessage(ResourceErrorMessages.CNPJ_IS_REQUIRED)
            .Matches(@"^\d{2}\.?\d{3}\.?\d{3}/?\d{4}-?\d{2}$")
            .WithMessage("cnpj invalid");
        RuleFor(c => c.PhoneNumber).NotEmpty()
            .WithMessage(ResourceErrorMessages.PHONE_NUMBER_IS_REQUIRED)
            .Matches(@"^\(?\d{2}\)?\s?\d{4,5}-?\d{4}$")
            .WithMessage(ResourceErrorMessages.INVALID_PHONE_NUMBER_FORMAT);
        RuleFor(c => c.AddressId).GreaterThan(0)
            .WithMessage(ResourceErrorMessages.ADDRESSID_MUST_BE_GREATER_THAN_0);
    }
}
