using FleetManager.Communication.Request.ToContractTemplate;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToContractTemplate;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToContractTemplate.Update
{
    public class UpdateContractTemplateUseCase(
        IContractTemplateWriteOnlyRepository repository,
        IUnitOfWork unitOfWork) : IUpdateContractTemplateUseCase
    {
        public async Task Execute(long id, RequestContractTemplateJson request)
        {
            Validate(request);

            var template = await repository.GetById(id) ??
                throw new NotFoundException("ResourceErrorMessages.CONTRACT_TEMPLATE_NOT_FOUND");

            // Se o template já está ativo, não editamos "no lugar" — isso mudaria silenciosamente
            // o texto de contratos que ainda vão ser gerados a partir dele. Regra fica na própria
            // entidade (ContractTemplate.Update lança BusinessRuleException nesse caso).
            template.Update(request.Name, request.Content);

            repository.Update(template);
            await unitOfWork.Commit();
        }

        private static void Validate(RequestContractTemplateJson request)
        {
            var validator = new ContractTemplateValidator();
            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errors = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errors);
            }
        }
    }
}