using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.ToContract;
using FleetManager.Application.UseCase.ToContract.DetectOverdue;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Enum;
using Shouldly;

namespace UseCase.Tests.ToContract.DetectOverdue
{
    public class DetectOverdueContractsUseCaseTest
    {
        [Fact]
        public async Task Success_Marks_Active_Contracts_As_Overdue()
        {
            var overdueContract1 = ContractBuilder.Build(1, status: ContractStatus.Active);
            var overdueContract2 = ContractBuilder.Build(2, status: ContractStatus.Active);

            var useCase = CreateUseCase([overdueContract1, overdueContract2]);

            var totalMarked = await useCase.Execute();

            totalMarked.ShouldBe(2);
            overdueContract1.ContractStatus.ShouldBe(ContractStatus.Overdue);
            overdueContract2.ContractStatus.ShouldBe(ContractStatus.Overdue);
        }

        [Fact]
        public async Task Success_No_Overdue_Contracts()
        {
            var useCase = CreateUseCase([]);

            var totalMarked = await useCase.Execute();

            totalMarked.ShouldBe(0);
        }

        private static DetectOverdueContractsUseCase CreateUseCase(List<Contract> overdueContracts)
        {
            var contractRepositoryBuilder = new ContractWriteOnlyRepositoryBuilder()
                .GetActiveContractsPastDueDate(overdueContracts);

            foreach (var contract in overdueContracts)
                contractRepositoryBuilder.Update(contract);

            var contractRepository = contractRepositoryBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();

            return new DetectOverdueContractsUseCase(contractRepository, unitOfWork);
        }
    }
}
