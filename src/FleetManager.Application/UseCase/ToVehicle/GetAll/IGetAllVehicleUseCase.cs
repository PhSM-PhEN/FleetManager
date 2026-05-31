using FleetManager.Communication.Responses;

namespace FleetManager.Application.UseCase.ToVehicle.GetAll
{
    public interface IGetAllVehicleUseCase
    {
        Task<ResponseListVehicleJson> Execute();
    }
}
