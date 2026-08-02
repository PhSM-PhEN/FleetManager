using FleetManager.Communication.Request.ToContract;
using FleetManager.Communication.Response.ToContract;

namespace FleetManager.Application.UseCase.ToContract.Preview
{
    public interface IPreviewContractUseCase
    {
        Task<ResponseContractJson> Execute(RequestContractJson request);
    }
}
