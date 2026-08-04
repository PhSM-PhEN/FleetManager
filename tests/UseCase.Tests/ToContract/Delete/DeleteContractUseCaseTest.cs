using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.ToContract;
using FleetManager.Application.UseCase.ToContract.Delete;
using FleetManager.Domain.Entities;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.ToContract.Delete
{
    public class DeleteContractUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var contract = ContractBuilder.Build(1);
            var useCase = CreateUseCase(contract);

            await useCase.Execute(contract.Id);
        }

        [Fact]
        public async Task Error_Contract_Not_Found()
        {
            var useCase = CreateUseCase(contract: null);
            var act = async () => await useCase.Execute(999);

            var result = await act.ShouldThrowAsync<NotFoundException>();
            result.Message.ShouldBe(ResourceErrorMessages.CONTRACT_NOT_FOUND);
        }

        private static DeleteContractUseCase CreateUseCase(Contract? contract)
        {
            var repositoryBuilder = new ContractWriteOnlyRepositoryBuilder();

            if (contract is not null)
                repositoryBuilder.GetById(contract.Id, contract).Delete(contract);

            var repository = repositoryBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();

            return new DeleteContractUseCase(repository, unitOfWork);
        }
    }
}
