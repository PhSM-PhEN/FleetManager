namespace FleetManager.Application.UseCase.ToTenant.Deactivate
{
    public interface IDeactivateTenantUseCase
    {
        Task Execute(long id);
    }
}
