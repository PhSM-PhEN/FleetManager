using FleetManager.Communication.Responses;

namespace FleetManager.Application.UseCase.ToVehicle.GetAll
{
    public interface IGetAllVehicleUseCase
    {
        Task<ResponsePaginatedJson<ResponseVehicleJson>> Execute(int pageNumber, int pageSize);
    }
}
