using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.ToContract;
using FleetManager.Application.UseCase.ToContract.Cancel;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Enum;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.ToContract.Cancel
{
    public class CancelContractUseCaseTest
    {
        [Fact]
        public async Task Success_From_Reserved()
        {
            var contract = ContractBuilder.Build(1, status: ContractStatus.Reserved);
            var useCase = CreateUseCase(contract);

            await useCase.Execute(contract.Id);

            contract.ContractStatus.ShouldBe(ContractStatus.Cancelled);
        }

        [Fact]
        public async Task Success_From_Active()
        {
            var contract = ContractBuilder.Build(1, status: ContractStatus.Active);
            var useCase = CreateUseCase(contract);

            await useCase.Execute(contract.Id);

            contract.ContractStatus.ShouldBe(ContractStatus.Cancelled);
        }

        [Fact]
        public async Task Error_Contract_Not_Found()
        {
            var useCase = CreateUseCase(contract: null);
            var act = async () => await useCase.Execute(999);

            var result = await act.ShouldThrowAsync<NotFoundException>();
            result.Message.ShouldBe(ResourceErrorMessages.CONTRACT_NOT_FOUND);
        }

        [Fact]
        public async Task Error_Contract_Already_Finished()
        {
            var contract = ContractBuilder.Build(1, status: ContractStatus.Finished);
            var useCase = CreateUseCase(contract);
            var act = async () => await useCase.Execute(contract.Id);

            var result = await act.ShouldThrowAsync<BusinessRuleException>();
            result.Message.ShouldBe(ResourceErrorMessages.CONTRACT_NOT_ACTIVE);
        }

        private static CancelContractUseCase CreateUseCase(Contract? contract)
        {
            var repositoryBuilder = new ContractWriteOnlyRepositoryBuilder();

            if (contract is not null)
            {
                repositoryBuilder.GetById(contract.Id, contract);
                repositoryBuilder.Update(contract);
            }
            else
            {
                repositoryBuilder.GetById(999, null);
            }

            var repository = repositoryBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();

            return new CancelContractUseCase(repository, unitOfWork);
        }
    }
}
