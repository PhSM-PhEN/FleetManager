namespace FleetManager.Application.UseCase.ToClient.Disable
{
    public interface IDisableClientUseCase
    {
        Task Execute(long id);
    }
}
