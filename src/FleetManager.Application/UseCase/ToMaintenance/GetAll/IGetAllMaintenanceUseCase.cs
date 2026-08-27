using FleetManager.Communication.Response;
using FleetManager.Communication.Response.ToMaintenance;

namespace FleetManager.Application.UseCase.ToMaintenance.GetAll
{
    public interface IGetAllMaintenanceUseCase
    {
        Task<ResponsePaginatedJson<ResponseShortMaintenanceJson>> Execute(int pageNumber, int pageSize);
    }
}
