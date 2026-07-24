using FleetManager.Communication.Response;
using FleetManager.Communication.Response.ToVehicle;

namespace FleetManager.Application.UseCase.ToVehicle.GetAll
{
    public interface IGetAllVehicleUseCase
    {
        Task<ResponsePaginatedJson<ResponseShortVehicleJson>> Execute(int pageNumber, int pageSize);
    }
}
