using System.Threading.Tasks;
using FleetManager.Communication.Request.ToContract;
using FleetManager.Communication.Response.ToContract;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToRentalPlan;
using FleetManager.Domain.Repositories.ToTenant;
using FleetManager.Domain.Repositories.ToVehicle;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToContract.Register
{
    public class RegisterContractUseCase(
        IVehicleReadOnlyRepository vehicleRepository, ITenanteReadOnlyRepository tenanteRepository,
        IRentalPlanReadOnlyRepository rentalPlanRepository
    ) : IRegisterContractUseCase
    {
        public Task<ResponseShortContractJson> Execute(RequestContractJson request)
        {
            var Vehicle = EnsureVehicleExist(request.VehicleId);
            var tenant = EnsureTenantExist(request.TenantId);
            var rentalPlan = EnsureRentalPlanExist(request.RentalPlanId);

            return null;
            
            
        }
        private async Task<Vehicle> EnsureVehicleExist(long id)
        {   
            return  await vehicleRepository.GetById(id) ??
               throw new NotFoundException(ResourceErrorMessages.VEHICLE_NOT_FOUND);
        }
        private async Task<Tenant> EnsureTenantExist(long id)
        {
            return await tenanteRepository.GetById(id) ??
                throw new NotFoundException(ResourceErrorMessages.TENANT_NOT_FOUND);
        }
        private async Task<RentalPlan> EnsureRentalPlanExist(long id)
        {
            return await rentalPlanRepository.GetById(id) 
                    ?? throw new NotFoundException(ResourceErrorMessages.RENTAL_PLAN_NOT_FOUND);
        }
    }
}
