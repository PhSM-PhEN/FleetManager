using FleetManager.Application.Extensions;
using FleetManager.Communication.Response;
using FleetManager.Communication.Response.ToMaintenance;
using FleetManager.Domain.Repositories.ToMaintenance;

namespace FleetManager.Application.UseCase.ToMaintenance.GetAll
{
    public class GetAllMaintenanceUseCase(IMaintenanceReadOnlyRepository repository) : IGetAllMaintenanceUseCase
    {
        public async Task<ResponsePaginatedJson<ResponseShortMaintenanceJson>> Execute(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
                pageNumber = 1;
            if (pageSize <= 0)
                pageSize = 10;

            var (maintenances, totalCount) = await repository.GetAll(pageNumber, pageSize);

            return new ResponsePaginatedJson<ResponseShortMaintenanceJson>
            {
                Data = maintenances.ToResponse(),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
    }
}
