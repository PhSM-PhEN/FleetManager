
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToContract;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToContract.Delete
{
    public class DeleteContractUseCase(IContractWriteOnlyRepository repository, IUnitOfWork unitOfWork) : IDeleteContractUseCase
    {
        public async Task Execute(long id)
        {
            var contract = await repository.GetById(id) ??
                throw new NotFoundException(ResourceErrorMessages.CONTRACT_NOT_FOUND);

            await repository.Delete(contract.Id);
            await unitOfWork.Commit();
        }
    }
}
