using FleetManager.Communication.Request.ToRentalPlan;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToRentalPlan;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToRentalPlan.Update
{
    public class UpdateRentalPlanUseCase(
        IRentalPlanWriteOnlyRepository repository,
        IUnitOfWork unitOfWork) : IUpdateRentalPlanUseCase
    {
        public async Task Execute(long vehicleId, RequestRentalPlanJson request)
        {
            Validate(request);

            var rentalPlan = await repository.GetById(vehicleId) ??
                throw new NotFoundException(ResourceErrorMessages.RENTAL_PLAN_NOT_FOUND);

            rentalPlan.Update(
                request.DailyPrice,
                request.MonthlyPrice,
                request.ExcessMileageRate,
                request.MileagePerDay,
                request.MileagePerMonthly);

            repository.Update(rentalPlan);
            await unitOfWork.Commit();
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
