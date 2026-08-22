using FleetManager.Communication.Response;
using FleetManager.Communication.Response.ToTenant;

namespace FleetManager.Application.UseCase.ToTenant.GetAll
{
    public interface IGetAllTenantUseCase
    {
        Task<ResponsePaginatedJson<ResponseShortTenantJson>> Execute(int pageNumber, int pageSize);
    }
}
