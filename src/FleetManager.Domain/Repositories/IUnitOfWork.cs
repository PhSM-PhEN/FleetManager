namespace FleetManager.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
