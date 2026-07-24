using FleetManager.Communication.Response.ToVehicle;

namespace FleetManager.Application.UseCase.ToVehicle.GetById
{
    public interface IGetByIdVehicleUseCase
    {
        Task <ResponseVehicleJson> Execute(long id);
    }
}
