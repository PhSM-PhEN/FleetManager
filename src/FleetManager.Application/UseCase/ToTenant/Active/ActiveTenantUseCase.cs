using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToTenant;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToTenant.Active
{
    public class ActiveTenantUseCase(ITenantWriteOnlyRepository repository, IUnitOfWork unitOfWork) : IActiveTenantUseCse
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
