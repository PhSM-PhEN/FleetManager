using FleetManager.Communication.Request.ToContract;
using FleetManager.Communication.Response.ToContract;

namespace FleetManager.Application.UseCase.ToContract.Preview
{
    public interface IPreviewContractUseCase
    {
        Task<ResponsePreviewContractJson> Execute(RequestPreviewContractJson request);
    }
}
