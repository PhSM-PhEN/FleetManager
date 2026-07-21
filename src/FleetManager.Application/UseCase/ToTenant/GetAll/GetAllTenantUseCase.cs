using FleetManager.Application.Extensions;
using FleetManager.Communication.Response;
using FleetManager.Communication.Response.ToRenant;
using FleetManager.Domain.Repositories.ToTenant;

namespace FleetManager.Application.UseCase.ToTenant.GetAll
{
    public class GetAllTenantUseCase(ITenanteReadOnlyRepository repository) : IGetAllTenantUseCase
    {
        public async Task<ResponsePaginatedJson<ResponseTenantJson>> Execute(int pageNumber, int pageSize)
        {
            if(pageNumber <= 0) pageNumber = 1;
            if(pageSize <= 0 || pageSize > 50) pageSize = 10;

            var(tenant, totalCount) = await repository.GetAll(pageNumber, pageSize);

            return new ResponsePaginatedJson<ResponseTenantJson>
            {
                Data = tenant.ToResponse(),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
    }
}
