using FleetManager.Application.Extensions;
using FleetManager.Communication.Response.ToRenant;
using FleetManager.Domain.Repositories.ToTenant;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToTenant.GetById
{
    public class GetByIdTenantUseCase(ITenanteReadOnlyRepository repository) : IGetByIdTenantUseCase
    {
        public async Task<ResponseTenantJson> Execute(long id)
        {
            var tenant = await repository.GetById(id) ?? 
                         throw new NotFoundException("not encontred");
                         
            return tenant.ToInfoResponse();
        }
    }
}
