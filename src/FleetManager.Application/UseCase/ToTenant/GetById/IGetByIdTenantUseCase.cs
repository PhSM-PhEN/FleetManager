using FleetManager.Communication.Response.ToTenant;

namespace FleetManager.Application.UseCase.ToTenant.GetById
{
    public interface IGetByIdTenantUseCase
    {
        Task<ResponseTenantJson> Execute(long id);
    }
}
