namespace FleetManager.Application.UseCase.ToTenant.Activate
{
    public interface IActivateTenantUseCase
    {
        Task Execute(long id);
    }
}
