using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.ToContract;
using FleetManager.Application.UseCase.ToContract.Activate;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Enum;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.ToContract.Activate
{
    public class ActivateContractUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var contract = ContractBuilder.Build(1, status: ContractStatus.Reserved);
            var useCase = CreateUseCase(contract);

            await useCase.Execute(contract.Id);

            contract.Status.ShouldBe(ContractStatus.Active);
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
        public async Task Error_Contract_Not_Reserved()
        {
            var contract = ContractBuilder.Build(1, status: ContractStatus.Active);
            var useCase = CreateUseCase(contract);
            var act = async () => await useCase.Execute(contract.Id);

            var result = await act.ShouldThrowAsync<BusinessRuleException>();
            result.Message.ShouldBe(ResourceErrorMessages.CONTRACT_NOT_RESERVED);
        }

        private static ActivateContractUseCase CreateUseCase(Contract? contract)
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

            return new ActivateContractUseCase(repository, unitOfWork);
        }
    }
}
