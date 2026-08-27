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

        public async Task<ResponseContractDocumentJson> Execute(long contractId)
        {
            var contract = await contractRepository.GetById(contractId) ??
                throw new NotFoundException(ResourceErrorMessages.CONTRACT_NOT_FOUND);

            var template = await templateRepository.GetActive() ??
                throw new BusinessRuleException("ResourceErrorMessages.NO_ACTIVE_CONTRACT_TEMPLATE");

            var resolvedContent = ContractDocumentPlaceholderResolver.Resolve(template.Content, contract.ToInfoResponse());

            EnsureNoUnresolvedPlaceholders(resolvedContent);

            var document = new ContractDocument(contract.Id, template, resolvedContent);

            await documentRepository.Add(document);
            await unitOfWork.Commit();

            return new ResponseContractDocumentJson
            {
                Contract = contract.ToInfoResponse() ,
                TemplateVersion = template.Version,
                Content = resolvedContent,
                GeneratedAt = document.GeneratedAt
            };
        }

        private static void EnsureNoUnresolvedPlaceholders(string resolvedContent)
        {
            if (UnresolvedPlaceholderRegex.IsMatch(resolvedContent))
                throw new BusinessRuleException("ResourceErrorMessages.CONTRACT_TEMPLATE_HAS_UNRESOLVED_PLACEHOLDERS");
        }
    }
}