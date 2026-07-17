
using FleetManager.Application.Extensions;
using FleetManager.Communication.Response;
using FleetManager.Communication.Response.ToAddress;
using FleetManager.Domain.Repositories.ToAddress;

namespace FleetManager.Application.UseCase.ToAddress.GetAll
{
    public class GetAllAddressUseCase(IAddressReadOnlyRepository repository) : IGetAllAddressUseCase
    {
        public async Task<ResponsePaginatedJson<ResponseShortAddressJson>> Execute(int pageNumber, int pageSize)
        {
            if(pageNumber <= 0) pageNumber = 1;
            if(pageSize <= 0 || pageSize > 50) pageSize = 10;

            var (addres , totalCount) = await repository.GetAll(pageNumber, pageSize);


            return new ResponsePaginatedJson<ResponseShortAddressJson>
            {
                Data = addres.ToShortResponse(),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
            };
        }
    }
}
