namespace FleetManager.Application.UseCase.ToTenant.Delete
{
    public interface IDeleteTenantUseCase
    {
        Task Execute(long id);
    }
}
