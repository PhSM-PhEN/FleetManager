using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToContract;

namespace FleetManager.Application.UseCase.ToContract.DetectOverdue
{
    public class DetectOverdueContractsUseCase(
        IContractWriteOnlyRepository contractRepository,
        IUnitOfWork unitOfWork) : IDetectOverdueContractsUseCase
    {
        public async Task<int> Execute()
        {
            var overdueContracts = await contractRepository.GetActiveContractsPastDueDate(DateTime.UtcNow);

            foreach (var contract in overdueContracts)
            {
                contract.MarkAsOverdue();
                contractRepository.Update(contract);
            }

            if (overdueContracts.Count > 0)
                await unitOfWork.Commit();

            return overdueContracts.Count;
        }
    }
}
