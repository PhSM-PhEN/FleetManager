
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToRental;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToRental.Delete;

public class DeleteRentalUseCase(IRentalWriteOnlyRepository repository, 
IUnitOfWork unitOfWork ,IRentalUpdateOnlyRepository repositoryUpdate) : IDeleteRentalUseCase
{
    public async Task Execute(long id)
    {
        var rental = await repositoryUpdate.GetById(id);
        if(rental is null)
        {
            throw new NotFoundException("rental not found");
        }
        await repository.Delete(rental.Id);
        await unitOfWork.Commit();
    }
}
