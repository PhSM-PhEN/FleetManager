using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToContract;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToContract.Cancel
{
    public class CancelContractUseCase(
        IContractWriteOnlyRepository contractRepository,
        IUnitOfWork unitOfWork) : ICancelContractUseCase
    {
        public async Task Execute(long id)
        {
            var contract = await contractRepository.GetById(id) ??
                throw new NotFoundException(ResourceErrorMessages.CONTRACT_NOT_FOUND);

            contract.Cancel();

            contractRepository.Update(contract);
            await unitOfWork.Commit();
        }
    }
}
