using System;
using System.Data;
using FleetManager.communication.Requests.ToClient;
using FleetManager.Exception.ExceptionBase;
using FluentValidation;

namespace FleetManager.Application.UseCase.ToClient;

public class ClientValidate : AbstractValidator<RequestClientJson>
{
    public ClientValidate()
    {
        RuleFor(c => c.FirstAndLastName).NotEmpty()
            .WithMessage(ResourceErrorMessages.NAME_REQUIRED);
        RuleFor(c => c.RG).NotEmpty()
            .WithMessage(ResourceErrorMessages.RG_REQUIRED)
            .Matches(@"^[0-9A-Za-z.\s-]{5,14}$")
            .WithMessage(ResourceErrorMessages.RG_INVALID);
        RuleFor(c => c.CPF).NotEmpty()
            .WithMessage(ResourceErrorMessages.CPF_REQUIRED)
            .Matches(@"^\d{3}\.?\d{3}\.?\d{3}-?\d{2}$")
            .WithMessage(ResourceErrorMessages.CPF_INVALID);
        RuleFor(c => c.CnhRegisterNumber).NotEmpty()
            .WithMessage(ResourceErrorMessages.CNH_REGISTER_NUMEBER_REQUIRED)
            .Matches(@"^\{11}$")
            .WithMessage(ResourceErrorMessages.CNH_REGISTER_NUMBER_INVALID);
        RuleFor(c => c.CnhCategory).NotEmpty()
            .WithMessage(ResourceErrorMessages.CNH_CATEGORY_REQUIRED)
            .Matches(@"^(?i)(A|B|C|D|E|AB|AC|AD|AE)$")
            .WithMessage(ResourceErrorMessages.CNH_CATEGORY_INVALID);
    }
}
