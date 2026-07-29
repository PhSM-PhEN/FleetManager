using FleetManager.Communication.Request.ToContract;
using FleetManager.Communication.Response.ToContract;

namespace FleetManager.Application.UseCase.ToContract.Register
{
    public interface IRegisterContractUseCase
    {
        Task<ResponseShortContractJson> Execute(RequestContractJson request);
    }
}
