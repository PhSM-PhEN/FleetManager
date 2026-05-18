using FleetManager.communication.Requests.ToClient;
using FleetManager.Exception.ExceptionBase;
using FluentValidation;

namespace FleetManager.Application.UseCase.ToClient
{
    public class ClientValidator : AbstractValidator<RequestClientJson>
    {
        public ClientValidator()
        {
            RuleFor(client => client.FirstAndLastName)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.NAME_REQUIRED)
                .MinimumLength(3)
                .WithMessage(ResourceErrorMessages.NAME_MUST_BE_GREATER_THAN_3_LETTERS);

            RuleFor(client => client.PhoneNumber)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.PHONE_NUMBER_IS_REQUERID)
                .Matches(@"^\(?\d{2}\)?\s?\d{4,5}-?\d{4}$")
                .WithMessage(ResourceErrorMessages.INVALID_PHONE_NUMBER_FORMAT);

            RuleFor(client => client.RG)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.RG_IS_REQUIRED)
                .Matches(@"^(\d{1,2}\.?\d{3}\.?\d{3}-?[0-9Xx]|\d{7,9})$")
                .WithMessage(ResourceErrorMessages.INVALID_RG_FORMAT);

            RuleFor(client => client.CPF)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.CPF_IS_REQUIRED)
                .Matches(@"^\d{3}\.?\d{3}\.?\d{3}-?\d{2}$")
                .WithMessage(ResourceErrorMessages.INVALID_CPF_FORMAT);

            RuleFor(client => client.CnhRegisterNumber)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.CNH_REGISTER_NUMBER_IS_REQUIRED)
                .Matches(@"^\d{11}$")
                .WithMessage(ResourceErrorMessages.INVALID_CNH_CATEGORY);

            RuleFor(client => client.CnhCategory)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.CNH_CATEGORY_IS_REQUIRED)
                .Matches(@"^(A|B|C|D|E|AB|AC|AD|AE)$")
                .WithMessage(ResourceErrorMessages.INVALID_CNH_CATEGORY);

            RuleFor(client => client.AddressId)
                .GreaterThan(0)
                .WithMessage(ResourceErrorMessages.ADDRESSID_MUST_BE_GREATER_THAN_0);
        }
    }
}