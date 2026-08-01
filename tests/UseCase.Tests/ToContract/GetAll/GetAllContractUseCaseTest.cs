using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories.ToContract;
using FleetManager.Application.UseCase.ToContract.GetAll;
using FleetManager.Domain.Entities;
using Shouldly;

namespace UseCase.Tests.ToContract.GetAll
{
    public class GetAllContractUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var contracts = ContractBuilder.Collection(3);

            var useCase = CreateUseCase(1, 10, contracts, contracts.Count);
            var result = await useCase.Execute(1, 10);

            result.ShouldNotBeNull();
            result.Data.Count.ShouldBe(contracts.Count);
            result.PageNumber.ShouldBe(1);
            result.PageSize.ShouldBe(10);
            result.TotalCount.ShouldBe(contracts.Count);
        }

        [Fact]
        public async Task Success_Empty_List()
        {
            var useCase = CreateUseCase(1, 10, [], 0);
            var result = await useCase.Execute(1, 10);

            result.ShouldNotBeNull();
            result.Data.ShouldBeEmpty();
            result.TotalCount.ShouldBe(0);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(-5, 1)]
        public async Task PageNumber_LessThanOrEqualZero_DefaultsTo_One(int requestedPage, int expectedPage)
        {
            var contracts = ContractBuilder.Collection(2);

            var useCase = CreateUseCase(expectedPage, 10, contracts, contracts.Count);
            var result = await useCase.Execute(requestedPage, 10);

            result.PageNumber.ShouldBe(expectedPage);
        }

        [Theory]
        [InlineData(0, 10)]
        [InlineData(-1, 10)]
        public async Task PageSize_LessThanOrEqualZero_DefaultsTo_Ten(int requestedSize, int expectedSize)
        {
            var contracts = ContractBuilder.Collection(2);

            var useCase = CreateUseCase(1, expectedSize, contracts, contracts.Count);
            var result = await useCase.Execute(1, requestedSize);

            result.PageSize.ShouldBe(expectedSize);
        }

        private static GetAllContractUseCase CreateUseCase(int pageNumber, int pageSize, List<Contract> contracts, int totalCount)
        {
            var repository = new ContractReadOnlyRepositoryBuilder()
                .GetAll(contracts, pageNumber, pageSize, totalCount)
                .Build();

            return new GetAllContractUseCase(repository);
        }
    }
}
