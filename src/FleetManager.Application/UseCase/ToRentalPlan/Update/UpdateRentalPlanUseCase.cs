using FleetManager.Communication.Requests;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToRentalPlans;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToRentalPlan.Update
{
    public class UpdateRentalPlanUseCase(IRentalPlansUpdateOnlyRepository repository, IUnitOfWork unitOfWork) : IUpdateRentalPlanUseCase
    {
        public async Task Execute(long id, RequestRentalPlansJson request)
        {
            Validate(request);
            var rentalPlan = await repository.GetById(id)
                    ?? throw new NotFoundException(ResourceErrorMessages.RENTAL_PLAN_NOT_FOUND);

            rentalPlan.Update(request.Name, (Domain.Enums.RentalMode)request.Mode, (Domain.Enums.TransmissionType)request.Transmission,
                              request.PriceRental, request.TotalKmIncluded, request.PricePerKm);

            repository.Update(rentalPlan);
            await unitOfWork.Commit();


        }
        public static void Validate(RequestRentalPlansJson request)
        {
            var validator = new RentalPlanValidator();
            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errorMessage = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessage);
            }
        }
    }
}
