using FleetManager.Application.Extensions;
using FleetManager.Communication.Request.ToContract;
using FleetManager.Communication.Response.ToContract;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Enum;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToContract;
using FleetManager.Domain.Repositories.ToRentalPlan;
using FleetManager.Domain.Repositories.ToTenant;
using FleetManager.Domain.Repositories.ToVehicle;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToContract.Register
{
    public class RegisterContractUseCase(
        IVehicleReadOnlyRepository vehicleRepository,
        ITenantReadOnlyRepository tenantRepository,
        IRentalPlanReadOnlyRepository rentalPlanRepository,
        IContractWriteOnlyRepository contractRepository,
        IUnitOfWork unitOfWork) : IRegisterContractUseCase
    {
        public async Task<ResponseShortContractJson> Execute(RequestContractJson request)
        {
            Validate(request);

            var vehicle = await EnsureVehicleExist(request.VehicleId);
            var tenant = await EnsureTenantExist(request.TenantId);
            var rentalPlan = await EnsureRentalPlanExist(request.RentalPlanId);

            if (vehicle.IsBlockedForMaintenance)
                throw new BusinessRuleException(ResourceErrorMessages.VEHICLE_BLOCKED_FOR_MAINTENANCE);
            if (!vehicle.IsActive)
                throw new BusinessRuleException(ResourceErrorMessages.VEHICLE_NOT_AVAILABLE);
            if (!tenant.IsActive)
                throw new BusinessRuleException(ResourceErrorMessages.TENANT_NOT_AVAILABLE);

            var hasActiveContract = await contractRepository.HasActiveContract(request.VehicleId);
            if (hasActiveContract)
                throw new BusinessRuleException(ResourceErrorMessages.VEHICLE_ALREADY_RENTED);


            var rentalType = Enum.Parse<RentalType>(request.RentalType);
            var (totalDays, _) = Contract.CalculatePeriod(rentalType, request.PickupDateTime, request.ReturnDueDateTime);

            var contract = new Contract(vehicle.Id, tenant.Id, rentalPlan,
                            rentalType, vehicle.CurrentMileage,
                            request.MileageContracted, request.TotalAmount,
                            request.PickupDateTime, request.ReturnDueDateTime);

            
            await contractRepository.Add(contract);
            await unitOfWork.Commit();

            return contract.ToResponse();
        }

        private async Task<Vehicle> EnsureVehicleExist(long id)
        {
            return await vehicleRepository.GetById(id) ??
                throw new NotFoundException(ResourceErrorMessages.VEHICLE_NOT_FOUND);
            
        }

        private async Task<Tenant> EnsureTenantExist(long id)
        {
            return await tenantRepository.GetById(id) ??
                throw new NotFoundException(ResourceErrorMessages.TENANT_NOT_FOUND);
        }

        private async Task<RentalPlan> EnsureRentalPlanExist(long id)
        {
            return await rentalPlanRepository.GetById(id) ??
                throw new NotFoundException(ResourceErrorMessages.RENTAL_PLAN_NOT_FOUND);
        }

        private static void Validate(RequestContractJson request)
        {
            var validator = new ContractValidator();
            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errors = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errors);
            }
        }
    }
}
