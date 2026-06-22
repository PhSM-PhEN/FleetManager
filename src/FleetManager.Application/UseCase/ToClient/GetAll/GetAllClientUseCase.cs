using AutoMapper;
using FleetManager.Communication.Responses;
using FleetManager.Domain.Repositories.ToClient;

namespace FleetManager.Application.UseCase.ToClient.GetAll;

public class GetAllClientUseCase(IMapper mapper, IClientReadOnlyRepository repository) : IGetAllClientUseCase
{
    public async Task<ResponsePaginatedJson<ResponseShortClientJson>> Execute(int pageNumber, int pageSize)
    {
        if(pageNumber <= 0)
            pageNumber = 1;
        if(pageSize <= 0)
            pageSize = 10;
        
        var (client, totalCount) = await repository.GetAll(pageNumber, pageSize);

        return new ResponsePaginatedJson<ResponseShortClientJson>
        {
            Data = mapper.Map<List<ResponseShortClientJson>>(client),
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount

        };
    }
}