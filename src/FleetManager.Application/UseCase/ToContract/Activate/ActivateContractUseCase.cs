using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToContract;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToContract.Activate
{
    public class ActivateContractUseCase(
        IContractWriteOnlyRepository contractRepository,
        IUnitOfWork unitOfWork) : IActivateContractUseCase
    {
        public async Task Execute(long id)
        {
            var contract = await contractRepository.GetById(id) ??
                throw new NotFoundException(ResourceErrorMessages.CONTRACT_NOT_FOUND);

            contract.Confirm();

            contractRepository.Update(contract);
            await unitOfWork.Commit();
        }
    }
}
