using FleetManager.Communication.Request.ToContract;
using FleetManager.Communication.Response.ToContract;

namespace FleetManager.Application.UseCase.ToContract.Complete
{
    public interface ICompleteContractUseCase
    {
        Task<ResponseCompleteContractJson> Execute(long id, RequestCompleteContractJson request);
    }
}
