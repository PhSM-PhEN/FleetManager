using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.ToMaintenance;
using FleetManager.Application.UseCase.ToMaintenance.Delete;
using FleetManager.Domain.Entities;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.ToMaintenance.Delete
{
    public class DeleteMaintenanceUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var maintenance = MaintenanceBuilder.Build(1);

            var useCase = CreateUseCase(maintenance);
            var act = async () => await useCase.Execute(maintenance.Id);

            await act.ShouldNotThrowAsync();
        }

        [Fact]
        public async Task Error_Maintenance_Not_Found()
        {
            var useCase = CreateUseCase(maintenance: null);
            var act = async () => await useCase.Execute(999);

            var result = await act.ShouldThrowAsync<NotFoundException>();
            result.Message.ShouldBe(ResourceErrorMessages.MAINTENANCE_NOT_FOUND);
        }

        private static DeleteMaintenanceUseCase CreateUseCase(Maintenance? maintenance)
        {
            var repositoryBuilder = new MaintenanceWriteOnlyRepositoryBuilder();

            if (maintenance is not null)
                repositoryBuilder.GetById(maintenance.Id, maintenance);

            var repository = repositoryBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();

            return new DeleteMaintenanceUseCase(repository, unitOfWork);
        }
    }
}
