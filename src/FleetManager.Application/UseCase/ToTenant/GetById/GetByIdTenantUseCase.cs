using FleetManager.Application.Extensions;
using FleetManager.Communication.Response.ToTenant;
using FleetManager.Domain.Repositories.ToTenant;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToTenant.GetById
{
    public class GetByIdTenantUseCase(ITenantReadOnlyRepository repository) : IGetByIdTenantUseCase
    {
        public async Task<ResponseTenantJson> Execute(long id)
        {
            var tenant = await repository.GetById(id) ?? 
                         throw new NotFoundException(ResourceErrorMessages.TENANT_NOT_FOUND);
                         
            return tenant.ToInfoResponse();
        }
    }
}
