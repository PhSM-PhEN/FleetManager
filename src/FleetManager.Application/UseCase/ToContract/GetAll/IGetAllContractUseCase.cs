using FleetManager.Communication.Response;
using FleetManager.Communication.Response.ToContract;

namespace FleetManager.Application.UseCase.ToContract.GetAll
{
    public interface IGetAllContractUseCase
    {
        Task<ResponsePaginatedJson<ResponseShortContractJson>> Execute(int pageNumber, int pageSize);
    }
}
