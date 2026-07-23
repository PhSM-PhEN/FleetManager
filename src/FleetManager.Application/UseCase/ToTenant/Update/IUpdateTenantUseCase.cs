using FleetManager.Communication.Request.ToTenant;

namespace FleetManager.Application.UseCase.ToTenant.Update
{
    public interface IUpdateTenantUseCase
    {
        Task Execute(long id, RequestUpdateTenantJson request);
    }
}
