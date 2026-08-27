using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToContractTemplate;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToContractTemplate.Activate
{
    public class ActivateContractTemplateUseCase(
        IContractTemplateWriteOnlyRepository repository,
        IUnitOfWork unitOfWork) : IActivateContractTemplateUseCase
    {
        public async Task Execute(long id)
        {
            var template = await repository.GetById(id) ??
                throw new NotFoundException(ResourceErrorMessages.CONTRACT_TEMPLATE_NOT_FOUND);

            if (template.IsActive)
                return; // já é o ativo, nada a fazer

            var currentlyActive = await repository.GetActive();
            currentlyActive?.Deactivate();

            template.Activate();

            if (currentlyActive is not null)
                repository.Update(currentlyActive);

            repository.Update(template);

            await unitOfWork.Commit();
        }
    }
}
