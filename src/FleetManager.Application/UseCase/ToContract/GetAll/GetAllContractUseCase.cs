using FleetManager.Application.Extensions;
using FleetManager.Communication.Response;
using FleetManager.Communication.Response.ToContract;
using FleetManager.Domain.Repositories.ToContract;

namespace FleetManager.Application.UseCase.ToContract.GetAll
{
    public class GetAllContractUseCase(IContractReadOnlyRepository repository) : IGetAllContractUseCase
    {
        public async Task<ResponsePaginatedJson<ResponseShortContractJson>> Execute(int pageNumber, int pageSize)
        {
            if(pageNumber <= 0)
            {
                pageNumber = 1;
            }
            if(pageSize <= 0)
            {
                pageSize = 10;
            }
            var (contract , totalCount ) = await repository.GetAll(pageNumber, pageSize);
           
            return new ResponsePaginatedJson<ResponseShortContractJson>
            {
                Data = contract.ToResponse(),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };

        }
    }
}
