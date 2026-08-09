using FleetManager.Communication.Request.ToContract;

namespace FleetManager.Application.UseCase.ToContract.Complete
{
    public interface ICompleteContractUseCase
    {
        Task Execute(long id, RequestCompleteContractJson request);
    }
}
