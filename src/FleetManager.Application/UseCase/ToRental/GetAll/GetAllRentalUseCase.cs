using FleetManager.Application.Extensions;
using FleetManager.Communication.Responses;
using FleetManager.Domain.Repositories.ToRental;


namespace FleetManager.Application.UseCase.ToRental.GetAll
{
    public class GetAllRentalUseCase(IRentalReadOnlyRepository repository) : IGetAllRentalUseCase
    {
        public async Task<ResponsePaginatedJson<ResponseShortRentalJson>> Execute(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0) 
                pageNumber = 1;
            if (pageSize <= 0 || pageSize > 50)
                pageSize = 10;


            var (rental, totalcount) = await repository.GetAll(pageNumber, pageSize);
      
            return new ResponsePaginatedJson<ResponseShortRentalJson>
            {
                Data = rental.ToResponse(),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalcount       
                
            
            };
        }
    };
    
   

}
