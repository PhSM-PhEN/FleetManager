using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToTenant;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToTenant.Activate
{
    public class ActivateTenantUseCase(ITenantWriteOnlyRepository repository, IUnitOfWork unitOfWork) : IActivateTenantUseCase
    {
        public async Task Execute(long id)
        {
            var tenant = await repository.GetById(id) ?? throw new NotFoundException(ResourceErrorMessages.TENANT_NOT_FOUND);
            tenant.Activate();
            repository.Update(tenant);
            await unitOfWork.Commit();
        }
    }
}
