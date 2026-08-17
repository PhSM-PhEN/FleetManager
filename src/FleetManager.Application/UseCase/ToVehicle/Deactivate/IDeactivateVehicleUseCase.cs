namespace FleetManager.Application.UseCase.ToVehicle.Deactivate
{
    public interface IDeactivateVehicleUseCase
    {
        Task Execute(long id);
    }
}
