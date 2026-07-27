using FleetManager.Communication.Request.ToVehiclePricing;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToVehiclePricing;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToVehiclePricing.Update
{
    public class UpdateVehiclePricingUseCase(
        IVehiclePricingWriteOnlyRepository repository,
        IUnitOfWork unitOfWork) : IUpdateVehiclePricingUseCase
    {
        public async Task Execute(long vehicleId, RequestVehiclePricingJson request)
        {
            Validate(request);

            var pricing = await repository.GetById(vehicleId) ??
                throw new NotFoundException(ResourceErrorMessages.VEHICLE_PRICING_NOT_FOUND);

            pricing.Update(
                request.DailyPrice,
                request.MonthlyPrice,
                request.ExcessMileageRate,
                request.MileagePerDay,
                request.MileagePerMonthly);

            repository.Update(pricing);
            await unitOfWork.Commit();
        }

        private static void Validate(RequestVehiclePricingJson request)
        {
            var validator = new VehiclePricingValidator();
            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errors = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errors);
            }
        }
    }
}
