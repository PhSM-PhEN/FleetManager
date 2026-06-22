using AutoMapper;
using FleetManager.Communication.Responses;
using FleetManager.Domain.Repositories.ToRental;


namespace FleetManager.Application.UseCase.ToRental.GetAll;

public class GetAllRentalUseCase(IRentalReadOnlyRepository repository, IMapper mapper) : IGetAllRentalUseCase
{
    public async Task<ResponsePaginatedJson<ResponseRentalJson>> Execute(int pageNumber, int pageSize)
    {
        if (pageNumber <= 0) 
            pageNumber = 1;
        if (pageSize <= 0 || pageSize > 50)
            pageSize = 10;


        var (rental, totalcount) = await repository.GetAll(pageNumber, pageSize);
      
        return new ResponsePaginatedJson<ResponseRentalJson>
        {
            Data = mapper.Map<List<ResponseRentalJson>>(rental),
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalcount       
                
            
        };
    }
};
    
   

