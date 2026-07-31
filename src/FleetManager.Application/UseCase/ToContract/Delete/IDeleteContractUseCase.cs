namespace FleetManager.Application.UseCase.ToContract.Delete
{
    public interface IDeleteContractUseCase
    {
        Task Execute(long id);
    }
}
