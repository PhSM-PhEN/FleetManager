using FleetManager.Application.UseCase.ToContract.GenerateDocument;
using FleetManager.Communication.Request.ToContractTemplate;
using FleetManager.Exception.ExceptionBase;
using FluentValidation;
using System.Text.RegularExpressions;

namespace FleetManager.Application.UseCase.ToContractTemplate
{
    public class ContractTemplateValidator : AbstractValidator<RequestContractTemplateJson>
    {
        private static readonly Regex PlaceholderRegex = new(@"\{\{.*?\}\}", RegexOptions.Compiled);

        public ContractTemplateValidator()
        {
            RuleFor(t => t.Name).NotEmpty().WithMessage(ResourceErrorMessages.NAME_IS_REQUIRED);
            RuleFor(t => t.Content).NotEmpty().WithMessage(ResourceErrorMessages.CONTRACT_TEMPLATE_CONTENT_REQUIRED);

            RuleFor(t => t.Content).Custom((content, context) =>
            {
                if (string.IsNullOrWhiteSpace(content))
                    return;

                var found = PlaceholderRegex.Matches(content).Select(m => m.Value).Distinct();
                var unknown = found.Except(ContractPlaceholders.All).ToList();

                if (unknown.Count > 0)
                    context.AddFailure("Content",
                        $"{ResourceErrorMessages.CONTRACT_TEMPLATE_UNKNOWN_PLACEHOLDERS}: {string.Join(", ", unknown)}");
            });
        }
    }
}
