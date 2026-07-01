using FleetManager.Communication.Requests;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToRental;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToRental.Update
{
    public class UpdateRentalUseCase(IRentalUpdateOnlyRepository repository, IUnitOfWork unitOfWork) : IUpdateRentalUseCase
    {
        public async Task Execute(long id, RequestUpdateRentJson request)
        {
            Validate(request);
            var rental = await repository.GetById(id)
                ?? throw new NotFoundException(ResourceErrorMessages.RENTAL_NOT_FOUND);

            rental.Reschedule(request.StartDate, request.EndDate);

            if (request.ExtraKm > 0)
                rental.AddExtraKm(request.ExtraKm);

            repository.Update(rental);
            await unitOfWork.Commit();


        }
        private static void Validate(RequestUpdateRentJson request)
        {
            var Validator = new RentalUpdateValidator();
            var result = Validator.Validate(request);

            if (result.IsValid == false)
            {
                var error = result.Errors.Select(erro => erro.ErrorMessage).ToList();
                throw new ErrorOnValidationException(error);
            }
        }
    }
}
