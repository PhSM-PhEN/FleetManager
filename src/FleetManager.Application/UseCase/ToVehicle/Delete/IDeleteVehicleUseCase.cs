namespace FleetManager.Application.UseCase.ToVehicle.Delete
{
    public interface IDeleteVehicleUseCase
    {
        Task Execute(long id);
    }
}
