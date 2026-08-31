using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToContractTemplate;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToContractTemplate.Deactivate
{
    public class DeactivateContractTemplateUseCase(
        IContractTemplateWriteOnlyRepository repository,
        IUnitOfWork unitOfWork) : IDeactivateContractTemplateUseCase
    {
        public async Task Execute(long id)
        {
            var template = await repository.GetById(id) ??
                throw new NotFoundException(ResourceErrorMessages.CONTRACT_TEMPLATE_NOT_FOUND);

            if (!template.IsActive)
                return; // já está inativo, nada a fazer

            template.Deactivate();
            repository.Update(template);

            await unitOfWork.Commit();
        }
    }
}
