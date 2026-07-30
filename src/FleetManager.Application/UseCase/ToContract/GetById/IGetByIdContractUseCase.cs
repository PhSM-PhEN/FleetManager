using FleetManager.Communication.Response.ToContract;

namespace FleetManager.Application.UseCase.ToContract.GetById
{
    public interface IGetByIdContractUseCase
    {
        Task<ResponseContractJson> Execute(long id);
    }
}
