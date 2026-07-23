using FleetManager.Communication.Response.ToRenant;

namespace FleetManager.Application.UseCase.ToTenant.GetById
{
    public interface IGetByIdTenantUseCase
    {
        Task<ResponseTenantJson> Execute(long id);
    }
}
