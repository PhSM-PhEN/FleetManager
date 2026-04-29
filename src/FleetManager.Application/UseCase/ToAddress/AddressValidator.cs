using System;
using System.Data;
using FleetManager.communication.Requests.ToAddress;
using FluentValidation;

namespace FleetManager.Application.UseCase.ToAddress;

public class AddressValidator : AbstractValidator<RequestAddressJson>
{
    public AddressValidator()
    {
        RuleFor(x => x.Street).NotEmpty().WithMessage("Street required");
        RuleFor(x => x.Number).NotEmpty().WithMessage("Number required");
        RuleFor(x => x.City).NotEmpty().WithMessage("City required");
        RuleFor(x => x.State).NotEmpty().WithMessage("State required");
        RuleFor(x => x.ZipCode).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("ZipCode required")
        .Matches(@"^\d{5}-?\d{3}$")
            .WithMessage("ZipCode invalid");

    }
}
