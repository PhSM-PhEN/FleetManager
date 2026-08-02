namespace FleetManager.Application.UseCase.ToContract.Cancel
{
    public interface ICancelContractUseCase
    {
        Task Execute(long id);
    }
}
