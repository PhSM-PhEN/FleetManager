using FleetManager.Application.Extensions;
using FleetManager.Communication.Response;
using FleetManager.Communication.Response.ToVehicle;
using FleetManager.Domain.Repositories.ToVehicle;

namespace FleetManager.Application.UseCase.ToVehicle.GetAll
{
    public class GetAllVehicleUseCase(IVehicleReadOnlyRepository repository) : IGetAllVehicleUseCase
    {
        public async Task<ResponsePaginatedJson<ResponseShortVehicleJson>> Execute(int pageNumber, int pageSize)
        {
            if(pageNumber <= 0) pageNumber = 1;
            if(pageSize <= 0 || pageSize > 50) pageSize = 10;

            var (vehicle , totalCount) = await repository.GetAll(pageNumber,pageSize);
            
            return new ResponsePaginatedJson<ResponseShortVehicleJson>
            {
                Data = vehicle.ToResponse(),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
    }
}
