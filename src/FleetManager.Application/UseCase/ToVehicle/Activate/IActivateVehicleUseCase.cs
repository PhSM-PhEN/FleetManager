namespace FleetManager.Application.UseCase.ToVehicle.Activate
{
    public interface IActivateVehicleUseCase
    {
        Task Execute(long id);
    }
}
