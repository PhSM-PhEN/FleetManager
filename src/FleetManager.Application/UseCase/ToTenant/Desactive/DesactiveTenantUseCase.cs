using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToTenant;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToTenant.Desactive
{
    public class DesactiveTenantUseCase(ITenantWriteOnlyRepository repository, IUnitOfWork unitOfWork) : IDesactiveTenantUseCase
    {
        public async Task Execute(long id)
        {
            var tenant = await repository.GetById(id) ?? throw new NotFoundException(ResourceErrorMessages.TENANT_NOT_FOUND);
            tenant.Desactive();
            repository.Update(tenant);
            await unitOfWork.Commit();
        }
    }
}
