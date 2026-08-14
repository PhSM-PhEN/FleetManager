namespace FleetManager.Application.UseCase.ToMaintenance.Delete
{
    public interface IDeleteMaintenanceUseCase
    {
        Task Execute(long id);
    }
}
