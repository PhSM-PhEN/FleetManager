using FleetManager.Communication.Request.ToCompany;
using FleetManager.Exception.ExceptionBase;
using FluentValidation;

namespace FleetManager.Application.UseCase.ToCompany.UpdateTaxRegime;

public class TaxeRegimeValidator : AbstractValidator<RequestCompanyUpdateTaxRegimeJson>
{
    public TaxeRegimeValidator()
    {
        RuleFor(tx => tx.TaxRegime).Must(value => value == "SimplesNacional" || value == "PresumedProfit" || value == "ActualProfit")
                .WithMessage(ResourceErrorMessages.INVALID_TAXE_REGIME_FORMAT);
        
    }
}
