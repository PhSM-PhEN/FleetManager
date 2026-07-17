using FleetManager.Communication.Response;
using FleetManager.Communication.Response.ToAddress;

namespace FleetManager.Application.UseCase.ToAddress.GetAll
{
    public interface IGetAllAddressUseCase
    {
        Task<ResponsePaginatedJson<ResponseShortAddressJson>> Execute(int pageNumber, int pageSize);
    }
}
