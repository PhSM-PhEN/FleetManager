using FleetManager.Communication.Request.ToContract;
using FleetManager.Communication.Response.ToContract;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Enum;
using FleetManager.Domain.Repositories.ToContract;
using FleetManager.Domain.Repositories.ToTenant;
using FleetManager.Domain.Repositories.ToVehicle;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToContract.Preview
{

    public class PreviewContractUseCase(
        IVehicleReadOnlyRepository vehicleRepository,
        ITenantReadOnlyRepository tenantRepository,
        IContractWriteOnlyRepository contractRepository) : IPreviewContractUseCase
    {
        public async Task<ResponsePreviewContractJson> Execute(RequestPreviewContractJson request)
        {
            Validate(request);

            var vehicle = await EnsureVehicleExist(request.VehicleId);
            if (!vehicle.IsActive)
                throw new BusinessRuleException(ResourceErrorMessages.VEHICLE_NOT_AVAILABLE);

            var hasActiveContract = await contractRepository.HasActiveContract(request.VehicleId);
            if (hasActiveContract)
                throw new BusinessRuleException(ResourceErrorMessages.VEHICLE_ALREADY_RENTED);

            var tenant = await EnsureTenantExist(request.TenantId);
            if (!tenant.IsActive)
                throw new BusinessRuleException(ResourceErrorMessages.TENANT_NOT_AVAILABLE);

            var rentalPlan = vehicle.RentalPlan;
            var rentalType = Enum.Parse<RentalType>(request.RentalType);

            var (totalDays, returnDueDateTime) = Contract.CalculatePeriod(rentalType, request.PickupDateTime, request.ReturnDueDateTime);

            var mileageContracted = ContractTermsCalculator.GetMileageContracted(request.DesiredExcessMileage, rentalType, rentalPlan, totalDays);
            var totalAmount = ContractTermsCalculator.GetTotalAmount(request.DesiredExcessMileage, rentalType, rentalPlan, totalDays);
            
            return new ResponsePreviewContractJson
            {
                VehicleId = vehicle.Id,
                TenantId = tenant.Id,
                RentalPlanId = rentalPlan.Id,
                RentalType = request.RentalType,
                PickupDateTime = request.PickupDateTime,
                ReturnDueDateTime = returnDueDateTime,
                TotalDays = totalDays,
                MileageContracted = mileageContracted,
                TotalAmount = totalAmount
            };
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

        private static void Validate(RequestPreviewContractJson request)
        {
            var validator = new PreviewContractValidator();
            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errors = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errors);
            }
        }
    }
}
