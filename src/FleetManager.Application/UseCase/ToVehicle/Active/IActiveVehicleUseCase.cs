namespace FleetManager.Application.UseCase.ToVehicle.Active
{
    public interface IActiveVehicleUseCase
    {
        Task Execute(long id);
    }
}
