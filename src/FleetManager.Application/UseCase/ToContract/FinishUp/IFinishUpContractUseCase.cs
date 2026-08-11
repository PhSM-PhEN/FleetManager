using FleetManager.Communication.Request.ToContract;
using FleetManager.Communication.Response.ToContract;

namespace FleetManager.Application.UseCase.ToContract.FinishUp
{
    public interface IFinishUpContractUseCase
    {
        Task<ResponseFinishUpContractJson> Execute(long id, RequestFinishUpContractJson request);
    }
}
