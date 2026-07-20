using FleetManager.Communication.Request.ToTenant;
using FleetManager.Communication.Response.ToRenant;

namespace FleetManager.Application.UseCase.ToTenant.Register
{
    public interface IRegisterTenantUseCase
    {
        Task<ResponseRegiserTenantJson> Execute(RequestTenantJson request);
    }
}
