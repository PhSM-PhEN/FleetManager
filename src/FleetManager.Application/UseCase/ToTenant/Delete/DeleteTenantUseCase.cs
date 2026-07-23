using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToTenant;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToTenant.Delete
{
    public class DeleteTenantUseCase(ITenantWriteOnlyRepository repository, IUnitOfWork unitOfWork) : IDeleteTenantUseCase
    {
        public async Task Execute(long id)
        {
            var tenant = await repository.GetById(id) ??
                throw new NotFoundException(ResourceErrorMessages.TENANT_NOT_FOUND);
            await repository.Delete(tenant.Id);
            await unitOfWork.Commit();
        }
    }
}
