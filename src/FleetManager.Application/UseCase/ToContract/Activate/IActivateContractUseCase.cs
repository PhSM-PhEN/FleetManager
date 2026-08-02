namespace FleetManager.Application.UseCase.ToContract.Activate
{
    public interface IActivateContractUseCase
    {
        Task Execute(long id);
    }
}
