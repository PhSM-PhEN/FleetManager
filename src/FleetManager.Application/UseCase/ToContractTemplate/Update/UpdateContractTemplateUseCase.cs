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
        public async Task Execute(long id, RequestUpdateContractTemplateJson request)
        {
            Validate(request);

            var template = await repository.GetById(id) ??
                throw new NotFoundException(ResourceErrorMessages.CONTRACT_TEMPLATE_NOT_FOUND);

            // PATCH parcial: só sobrescreve o que veio no payload. O template pode
            // estar ativo ou não - editar não afeta contratos já gerados, pois o
            // ContractDocument congela Content + Version no momento da geração.
            template.Update(request.Name, request.Content);

            repository.Update(template);
            await unitOfWork.Commit();
        }

        private static void Validate(RequestUpdateContractTemplateJson request)
        {
            var validator = new ContractTemplateUpdateValidator();
            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errors = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errors);
            }
        }
    }
}
