namespace FleetManager.Application.UseCase.ToVehicle.Delete
{
    public interface IDeleteVehicleUseCase
    {
        Task Delete(long id);
    }
}
