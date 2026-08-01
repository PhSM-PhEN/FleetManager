using FleetManager.Application.Extensions;
using FleetManager.Communication.Response;
using FleetManager.Communication.Response.ToRentalPlan;
using FleetManager.Domain.Repositories.ToRentalPlan;

namespace FleetManager.Application.UseCase.ToRentalPlan.GetAll
{
    public class GetAllRentalPlanUseCase(IRentalPlanReadOnlyRepository repository) : IGetAllRentalPlanUseCase
    {
        public async Task<ResponsePaginatedJson<ResponseRentalPlanJson>> Execute(int pageNumber, int pageSize)
        {
            if(pageNumber <= 0)
                pageNumber = 1;
            if( pageSize <= 0 || pageSize > 50)
                pageSize = 10;

            var (rentalPlan, totalCount) = await repository.GetAll(pageNumber, pageSize);

            return new ResponsePaginatedJson<ResponseRentalPlanJson>
            {
                Data = rentalPlan.ToResponse(),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
    }
}
