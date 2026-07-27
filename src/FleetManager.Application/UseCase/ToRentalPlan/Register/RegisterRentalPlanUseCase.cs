using FleetManager.Application.Extensions;
using FleetManager.Communication.Request.ToRentalPlan;
using FleetManager.Communication.Response.ToRentalPlan;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToRentalPlan;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToVehiclePricing.Register
{
    public class RegisterRentalPlanUseCase(
        IRentalPlanWriteOnlyRepository repository,
        IUnitOfWork unitOfWork) : IRegisterRentalPlanUseCase
    {
        public async Task<ResponseRentalPlanJson> Execute(RequestRentalPlanJson request)
        {
            Validate(request);

            var rentalPlan = new RentalPlan(
                request.Name,
                request.DailyPrice,
                request.MonthlyPrice,
                request.ExcessMileageRate,
                request.MileagePerDay,
                request.MileagePerMonthly);

            await repository.Add(rentalPlan);
            await unitOfWork.Commit();

            return rentalPlan.ToResponse();
        }

        private static void Validate(RequestRentalPlanJson request)
        {
            var validator = new RentalPlanValidator();
            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errors = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errors);
            }
        }
    }
}
