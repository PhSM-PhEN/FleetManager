using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.ToMaintenance;
using CommonTestUtilities.Request.ToMaintenance;
using FleetManager.Application.UseCase.ToMaintenance.Close;
using FleetManager.Domain.Entities;
using FleetManager.Domain.EnumExtensions;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.ToMaintenance.Close
{
    public class CloseMaintenanceUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var maintenance = MaintenanceBuilder.Build(1);
            var request = RequestClosedMaintenanceJsonBuilder.Build(workshopBudget: 500, problemDescription: "Broken suspension");

            var useCase = CreateUseCase(maintenance);
            var result = await useCase.Execute(maintenance.Id, request);

            result.ShouldNotBeNull();
            result.Id.ShouldBe(maintenance.Id);
            result.VehicleId.ShouldBe(maintenance.VehicleId);
            result.IncidentReportId.ShouldBe(maintenance.IncidentReportId);
            result.WorkshopBudget.ShouldBe(request.WorkshopBudget);
            result.ProblemDescription.ShouldBe(request.ProblemDescription);
        }

        [Fact]
        public async Task Error_Maintenance_Not_Found()
        {
            var request = RequestClosedMaintenanceJsonBuilder.Build();

            var useCase = CreateUseCase(maintenance: null);
            var act = async () => await useCase.Execute(999, request);

            var result = await act.ShouldThrowAsync<NotFoundException>();
            result.Message.ShouldBe(ResourceErrorMessages.MAINTENANCE_NOT_FOUND);
        }

        [Fact]
        public async Task Error_Already_Closed()
        {
            var maintenance = MaintenanceBuilder.BuildClosed(1);
            var request = RequestClosedMaintenanceJsonBuilder.Build();

            var useCase = CreateUseCase(maintenance);
            var act = async () => await useCase.Execute(maintenance.Id, request);

            var result = await act.ShouldThrowAsync<BusinessRuleException>();
            result.Message.ShouldBe(ResourceErrorMessages.MAINTENANCE_IS_ALREADY_CLOSED);
        }

        [Fact]
        public async Task Error_WorkshopBudget_Not_Positive()
        {
            var maintenance = MaintenanceBuilder.Build(1);
            var request = RequestClosedMaintenanceJsonBuilder.Build(workshopBudget: 0);

            var useCase = CreateUseCase(maintenance);
            var act = async () => await useCase.Execute(maintenance.Id, request);

            var result = await act.ShouldThrowAsync<BusinessRuleException>();
            result.Message.ShouldBe(ResourceErrorMessages.MAINTENANCE_WORKSHOP_BUDGET_REQUIRED);
        }

        private static CloseMaintenanceUseCase CreateUseCase(Maintenance? maintenance)
        {
            var repositoryBuilder = new MaintenanceWriteOnlyRepositoryBuilder();

            if (maintenance is not null)
                repositoryBuilder.GetById(maintenance.Id, maintenance);

            var repository = repositoryBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();

            return new CloseMaintenanceUseCase(repository, unitOfWork);
        }
    }
}
