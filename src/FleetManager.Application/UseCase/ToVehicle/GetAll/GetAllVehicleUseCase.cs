using FleetManager.Application.Extensions;
using FleetManager.Communication.Responses;
using FleetManager.Domain.Repositories.ToVehicle;

namespace FleetManager.Application.UseCase.ToVehicle.GetAll
{
    public class GetAllVehicleUseCase( IVehicleReadOnlyRepository vehicleReadOnly) : IGetAllVehicleUseCase
    {

        public async Task<ResponsePaginatedJson<ResponseVehicleJson>> Execute(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0) 
                pageNumber = 1;
            if (pageSize <= 0 || pageSize > 50)
                pageSize = 10;

            var (vehicle, totalcount) = await vehicleReadOnly.GetAll(pageNumber, pageSize);

            return new ResponsePaginatedJson<ResponseVehicleJson>
            {
                Data = vehicle.ToShortResponse(),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalcount

            };

        }
    }
    
}
