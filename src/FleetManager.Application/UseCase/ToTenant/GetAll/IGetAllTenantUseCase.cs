using FleetManager.Communication.Response;
using FleetManager.Communication.Response.ToRenant;

namespace FleetManager.Application.UseCase.ToTenant.GetAll
{
    public interface IGetAllTenantUseCase
    {
        Task<ResponsePaginatedJson<ResponseTenantJson>> Execute(int pageNumber, int pageSize);
    }
}
