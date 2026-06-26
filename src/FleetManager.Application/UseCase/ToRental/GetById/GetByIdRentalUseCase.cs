using FleetManager.Application.Extensions;
using FleetManager.Communication.Responses;
using FleetManager.Domain.Repositories.ToRental;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToRental.GetById
{
    public class GetByIdRentalUseCase(IRentalReadOnlyRepository repository) : IGetByIdRentalUseCase
    {
        public async Task<ResponseRentalJson> Execute(long id)
        {
            var rental = await repository.GetById(id)
                ?? throw new NotFoundException(ResourceErrorMessages.RENTAL_NOT_FOUND);

            return rental.ToDeTailResponse();
        }
    }
}
