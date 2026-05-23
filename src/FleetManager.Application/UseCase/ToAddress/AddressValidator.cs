using System;
using System.Data;
using FleetManager.communication.Requests;
using FleetManager.Exception.ExceptionBase;
using FluentValidation;

namespace FleetManager.Application.UseCase.ToAddress;

public class AddressValidator : AbstractValidator<RequestAddressJson>
{
    public AddressValidator()
    {
        RuleFor(x => x.Street).NotEmpty().WithMessage(ResourceErrorMessages.STREET_REQUIRED);
        RuleFor(x => x.Number).NotEmpty().WithMessage(ResourceErrorMessages.NUMBER_REQUIRED);
        RuleFor(x => x.City).NotEmpty().WithMessage(ResourceErrorMessages.CITY_REQUIRED);
        RuleFor(x => x.State).NotEmpty().WithMessage(ResourceErrorMessages.STATE_REQUIRED);
        RuleFor(x => x.ZipCode).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(ResourceErrorMessages.ZIPCODE_REQUIRED)
        .Matches(@"^\d{5}-?\d{3}$")
            .WithMessage(ResourceErrorMessages.ZIPCODE_INVALID);

    }
}
