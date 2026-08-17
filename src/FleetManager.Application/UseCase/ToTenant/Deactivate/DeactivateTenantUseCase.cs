using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToTenant;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToTenant.Deactivate
{
    public class DeactivateTenantUseCase(ITenantWriteOnlyRepository repository, IUnitOfWork unitOfWork) : IDeactivateTenantUseCase
    {
        public async Task Execute(long id)
        {
            var tenant = await repository.GetById(id) ?? throw new NotFoundException(ResourceErrorMessages.TENANT_NOT_FOUND);
            tenant.Deactivate();
            repository.Update(tenant);
            await unitOfWork.Commit();
        }
    }
}
