using FleetManager.Communication.Request.ToContract;

namespace FleetManager.Application.UseCase.ToContract.Update
{
    public interface IUpdateContractUseCase
    {
        Task Execute(long id, RequestUpdateContractJson request);
    }
}
