using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories.ToMaintenance;
using FleetManager.Application.UseCase.ToMaintenance.GetAll;
using Shouldly;

namespace UseCase.Tests.ToMaintenance.GetAll
{
    public class GetAllMaintenanceUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var maintenances = MaintenanceBuilder.Collection(3);

            var repository = new MaintenanceReadOnlyRepositoryBuilder()
                .GetAll(maintenances, pageNumber: 1, pageSize: 10, totalCount: maintenances.Count)
                .Build();

            var useCase = new GetAllMaintenanceUseCase(repository);
            var result = await useCase.Execute(1, 10);

            result.ShouldNotBeNull();
            result.Data.Count.ShouldBe(maintenances.Count);
            result.TotalCount.ShouldBe(maintenances.Count);
            result.PageNumber.ShouldBe(1);
            result.PageSize.ShouldBe(10);
        }

        [Fact]
        public async Task Success_Defaults_PageNumber_When_Not_Positive()
        {
            var maintenances = MaintenanceBuilder.Collection(2);

            var repository = new MaintenanceReadOnlyRepositoryBuilder()
                .GetAll(maintenances, pageNumber: 1, pageSize: 10, totalCount: maintenances.Count)
                .Build();

            var useCase = new GetAllMaintenanceUseCase(repository);
            var result = await useCase.Execute(0, 10);

            result.PageNumber.ShouldBe(1);
        }

        [Fact]
        public async Task Success_Defaults_PageSize_When_Not_Positive()
        {
            var maintenances = MaintenanceBuilder.Collection(2);

            var repository = new MaintenanceReadOnlyRepositoryBuilder()
                .GetAll(maintenances, pageNumber: 1, pageSize: 10, totalCount: maintenances.Count)
                .Build();

            var useCase = new GetAllMaintenanceUseCase(repository);
            var result = await useCase.Execute(1, -5);

            result.PageSize.ShouldBe(10);
        }
    }
}
