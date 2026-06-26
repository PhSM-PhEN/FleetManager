using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToAddress;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToAddress.Delete
{
    public class DeleteAddressUseCase(IAddressUpdateOnlyRepository Repository,IAddressWriteOnlyRepository repository, IUnitOfWork unitOfWork) : IDeleteAddressUseCase
    {

        public async Task Execute(long id)
        {
            var address = await Repository.GetById(id)
                ?? throw new NotFoundException(ResourceErrorMessages.ADDRESS_NOT_FOUND);

            await repository.Delete(address.Id);
            await unitOfWork.Commit();

        
        }
    }
}
