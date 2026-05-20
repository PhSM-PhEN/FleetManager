namespace FleetManager.Application.UseCase.ToClient.Delete
{
    public interface IDeleteClientUseCase
    {
        Task Execute(long id);
    }
}
