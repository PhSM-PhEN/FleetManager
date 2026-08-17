namespace FleetManager.Application.UseCase.ToTenant.Desactive
{
    public interface IDesactiveTenantUseCase
    {
        Task Execute(long id);
    }
}
