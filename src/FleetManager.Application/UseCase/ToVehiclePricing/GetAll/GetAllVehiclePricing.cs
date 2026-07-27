using FleetManager.Application.Extensions;
using FleetManager.Communication.Response;
using FleetManager.Communication.Response.ToVehiclePricing;
using FleetManager.Domain.Repositories.ToVehiclePricing;

namespace FleetManager.Application.UseCase.ToVehiclePricing.GetAll
{
    public class GetAllVehiclePricing(IVehiclePricingReadOnlyRepository repository) : IGetAllVehiclePricing
    {
        public async Task<ResponsePaginatedJson<ResponseVehiclePricingJson>> Execute(int pageNumber, int pageSize)
        {
            if(pageNumber <= 0)
                pageNumber = 1;
            if( pageSize <= 0)
                pageSize = 10;

            var (vehiclePricing, totalCount) = await repository.GetAll(pageNumber, pageSize);

            return new ResponsePaginatedJson<ResponseVehiclePricingJson>
            {
                Data = vehiclePricing.ToResponse(),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
    }
}
