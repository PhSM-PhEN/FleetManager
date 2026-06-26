using FleetManager.Communication.Responses;

namespace FleetManager.Application.UseCase.ToClient.GetAll
{
    public interface IGetAllClientUseCase
    {
        Task<ResponsePaginatedJson<ResponseShortClientJson>> Execute(int pageNumber, int pageSize);
    }
}
