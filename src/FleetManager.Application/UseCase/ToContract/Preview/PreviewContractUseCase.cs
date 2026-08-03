using FleetManager.Application.Extensions;
using FleetManager.Communication.Request.ToContract;
using FleetManager.Communication.Response.ToContract;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Enum;
using FleetManager.Domain.EnumExtensions;
using FleetManager.Domain.Repositories.ToContract;
using FleetManager.Domain.Repositories.ToRentalPlan;
using FleetManager.Domain.Repositories.ToTenant;
using FleetManager.Domain.Repositories.ToVehicle;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToContract.Preview
{
    /// <summary>
    /// Roda exatamente a mesma regra de cálculo do RegisterContractUseCase (dias, km contratada,
    /// valor total), mas sem persistir nada — usado pra pré-preencher a tela antes do usuário
    /// confirmar o registro do contrato.
    /// </summary>
    public class PreviewContractUseCase(
        IVehicleReadOnlyRepository vehicleRepository,
        ITenantReadOnlyRepository tenantRepository,
        IRentalPlanReadOnlyRepository rentalPlanRepository,
        IContractWriteOnlyRepository contractRepository) : IPreviewContractUseCase
    {
        public async Task<ResponseContractJson> Execute(RequestContractJson request)
        {
            Validate(request);

            var vehicle = await EnsureVehicleExist(request.VehicleId);
            var tenant = await EnsureTenantExist(request.TenantId);
            var rentalPlan = await EnsureRentalPlanExist(request.RentalPlanId);

            var hasActiveContract = await contractRepository.HasActiveContract(request.VehicleId);
            if (hasActiveContract)
                throw new BusinessRuleException(ResourceErrorMessages.VEHICLE_ALREADY_RENTED);

            var rentalType = Enum.Parse<RentalType>(request.RentalType);

            var (totalDays, returnDueDateTime) = Contract.CalculatePeriod(rentalType, request.PickupDateTime, request.ReturnDueDateTime);

            var mileageContracted = ContractTermsCalculator.GetMileageContracted(request.MileageContracted, rentalType, rentalPlan, totalDays);
            var totalAmount = ContractTermsCalculator.GetTotalAmount(request.TotalAmount, rentalType, rentalPlan, totalDays);

            return new ResponseContractJson
            {
                Id = null,
                RentalType = rentalType.RentalTypeToString(),
                ContractStatus = ContractStatus.Reserved.ContractStatusToString(),
                PickupDateTime = request.PickupDateTime,
                ReturnDueDateTime = returnDueDateTime,
                ActualReturnDateTime = null,
                TotalDays = totalDays,
                StartMileage = vehicle.CurrentMileage,
                EndMileage = vehicle.CurrentMileage + mileageContracted,
                MileageContracted = mileageContracted,
                SnapshotPriceDailyRate = rentalPlan.DailyPrice,
                SnapshotPriceMonthlyRate = rentalPlan.MonthlyPrice,
                SnapshotPricePerExtraMileage = rentalPlan.ExcessMileageRate,
                TotalAmount = totalAmount,
                Vehicle = vehicle.ToResponse(),
                Tenant = tenant.ToInfoResponse(),
                RentalPlan = rentalPlan.ToResponse()
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
