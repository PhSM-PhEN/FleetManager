using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using FleetManager.Application.UseCase.ToVehicle.GetAll;
using Shouldly;

namespace UseCase.Tests.ToVehicle.GetAll
{
    public class GetAllVehicleUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var vehicles = VehicleBuilder.Collection(3);

            var useCase = CreateUseCase(1, 10, vehicles, vehicles.Count);
            var result = await useCase.Execute(1, 10);

            result.ShouldNotBeNull();
            result.Data.Count.ShouldBe(vehicles.Count);
            result.PageNumber.ShouldBe(1);
            result.PageSize.ShouldBe(10);
            result.TotalCount.ShouldBe(vehicles.Count);
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
            var vehicles = VehicleBuilder.Collection(2);

            var useCase = CreateUseCase(expectedPage, 10, vehicles, vehicles.Count);
            var result = await useCase.Execute(requestedPage, 10);

            result.PageNumber.ShouldBe(expectedPage);
        }

        [Theory]
        [InlineData(0, 10)]
        [InlineData(-1, 10)]
        [InlineData(51, 10)]
        public async Task PageSize_OutOfRange_DefaultsTo_Ten(int requestedSize, int expectedSize)
        {
            var vehicles = VehicleBuilder.Collection(2);

            var useCase = CreateUseCase(1, expectedSize, vehicles, vehicles.Count);
            var result = await useCase.Execute(1, requestedSize);

            result.PageSize.ShouldBe(expectedSize);
        }

        private static GetAllVehicleUseCase CreateUseCase(int pageNumber, int pageSize, List<FleetManager.Domain.Entities.Vehicle> vehicles, int totalCount)
        {
            var repository = new VehicleReadOnlyRepositoryBuilder()
                .GetAll(vehicles, pageNumber, pageSize, totalCount)
                .Build();

            return new GetAllVehicleUseCase(repository);
        }
    }
}
