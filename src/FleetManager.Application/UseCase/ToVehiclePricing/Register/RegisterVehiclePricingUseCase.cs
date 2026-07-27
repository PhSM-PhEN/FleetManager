using FleetManager.Application.Extensions;
using FleetManager.Communication.Request.ToVehiclePricing;
using FleetManager.Communication.Response.ToVehiclePricing;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToVehicle;
using FleetManager.Domain.Repositories.ToVehiclePricing;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToVehiclePricing.Register
{
    public class RegisterVehiclePricingUseCase(
        IVehiclePricingWriteOnlyRepository repository,
        IUnitOfWork unitOfWork) : IRegisterVehiclePricingUseCase
    {
        public async Task<ResponseVehiclePricingJson> Execute(RequestVehiclePricingJson request)
        {
            Validate(request);

            var pricing = new VehiclePricing(
                request.Name,
                request.DailyPrice,
                request.MonthlyPrice,
                request.ExcessMileageRate,
                request.MileagePerDay,
                request.MileagePerMonthly);

            await repository.Add(pricing);
            await unitOfWork.Commit();

            return pricing.ToResponse();
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
