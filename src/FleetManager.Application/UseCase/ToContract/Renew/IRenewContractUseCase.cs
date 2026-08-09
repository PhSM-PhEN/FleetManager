using FleetManager.Communication.Request.ToContract;
using FleetManager.Communication.Response.ToContract;

namespace FleetManager.Application.UseCase.ToContract.Renew
{
    public interface IRenewContractUseCase
    {
        Task<ResponseShortContractJson> Execute(long id, RequestRenewContractJson request);
    }
}
