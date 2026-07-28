using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories.ToRentalPlan;
using FleetManager.Application.UseCase.ToRentalPlan.GetAll;
using Shouldly;

namespace UseCase.Tests.ToRentalPlan.GetAll
{
    public class GetAllRentalPlanUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var rentalPlans = RentalPlanBuilder.Collection(3);

            var useCase = CreateUseCase(1, 10, rentalPlans, rentalPlans.Count);
            var result = await useCase.Execute(1, 10);

            result.ShouldNotBeNull();
            result.Data.Count.ShouldBe(rentalPlans.Count);
            result.PageNumber.ShouldBe(1);
            result.PageSize.ShouldBe(10);
            result.TotalCount.ShouldBe(rentalPlans.Count);
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
            var rentalPlans = RentalPlanBuilder.Collection(2);

            var useCase = CreateUseCase(expectedPage, 10, rentalPlans, rentalPlans.Count);
            var result = await useCase.Execute(requestedPage, 10);

            result.PageNumber.ShouldBe(expectedPage);
        }

        [Theory]
        [InlineData(0, 10)]
        [InlineData(-1, 10)]
        public async Task PageSize_LessThanOrEqualZero_DefaultsTo_Ten(int requestedSize, int expectedSize)
        {
            var rentalPlans = RentalPlanBuilder.Collection(2);

            var useCase = CreateUseCase(1, expectedSize, rentalPlans, rentalPlans.Count);
            var result = await useCase.Execute(1, requestedSize);

            result.PageSize.ShouldBe(expectedSize);
        }

        private static GetAllRentalPlanUseCase CreateUseCase(int pageNumber, int pageSize, List<FleetManager.Domain.Entities.RentalPlan> rentalPlans, int totalCount)
        {
            var repository = new RentalPlanReadOnlyRepositoryBuilder()
                .GetAll(rentalPlans, pageNumber, pageSize, totalCount)
                .Build();

            return new GetAllRentalPlanUseCase(repository);
        }
    }
}
