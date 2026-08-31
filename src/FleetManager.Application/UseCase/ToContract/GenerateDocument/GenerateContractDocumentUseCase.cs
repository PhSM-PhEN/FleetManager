using FleetManager.Application.Extensions;
using FleetManager.Communication.Response.ToContract;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToContract;
using FleetManager.Domain.Repositories.ToContractDocument;
using FleetManager.Domain.Repositories.ToContractTemplate;
using FleetManager.Exception.ExceptionBase;
using System.Text.RegularExpressions;

namespace FleetManager.Application.UseCase.ToContract.GenerateDocument
{
    public class GenerateContractDocumentUseCase(
        IContractReadOnlyRepository contractRepository,
        IContractTemplateReadOnlyRepository templateRepository,
        IContractDocumentWriteOnlyRepository documentRepository,
        IUnitOfWork unitOfWork) : IGenerateContractDocumentUseCase
    {
        private static readonly Regex UnresolvedPlaceholderRegex = new(@"\{\{.*?\}\}", RegexOptions.Compiled);

        public async Task<ResponseContractDocumentJson> Execute(long contractId, long contractTemplateId)
        {
            var contract = await contractRepository.GetById(contractId) ??
                throw new NotFoundException(ResourceErrorMessages.CONTRACT_NOT_FOUND);

            // Vários templates podem estar ativos ao mesmo tempo (ex.: locação, locação
            // com seguro, pagamento parcelado) — quem gera o documento escolhe qual usar.
            var template = await templateRepository.GetById(contractTemplateId) ??
                throw new NotFoundException(ResourceErrorMessages.CONTRACT_TEMPLATE_NOT_FOUND);

            if (!template.IsActive)
                throw new BusinessRuleException(ResourceErrorMessages.CONTRACT_TEMPLATE_NOT_ACTIVE);

            var resolvedContent = ContractDocumentPlaceholderResolver.Resolve(template.Content, contract.ToInfoResponse());

            EnsureNoUnresolvedPlaceholders(resolvedContent);

            var document = new ContractDocument(contract.Id, template, resolvedContent);

            await documentRepository.Add(document);
            await unitOfWork.Commit();

            return new ResponseContractDocumentJson
            {
                ContractId = contract.Id ,
                TemplateVersion = template.Version,
                Content = resolvedContent,
                GeneratedAt = document.GeneratedAt
            };
        }

        private static void EnsureNoUnresolvedPlaceholders(string resolvedContent)
        {
            if (UnresolvedPlaceholderRegex.IsMatch(resolvedContent))
                throw new BusinessRuleException(ResourceErrorMessages.CONTRACT_TEMPLATE_HAS_UNRESOLVED_PLACEHOLDERS);
        }
    }
}
