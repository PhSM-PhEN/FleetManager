using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories.ToMaintenance;
using FleetManager.Application.UseCase.ToMaintenance.GetById;
using FleetManager.Domain.Entities;
using FleetManager.Domain.EnumExtensions;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.ToMaintenance.GetById
{
    public class GetByIdMaintenanceUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var vehicle = VehicleBuilder.Build(1);
            var incidentReport = IncidentReportBuilder.Build(1);
            var maintenance = MaintenanceBuilder.Build(1, vehicleId: vehicle.Id, incidentReportId: incidentReport.Id)
                .WithVehicle(vehicle)
                .WithIncidentReport(incidentReport);

            var useCase = CreateUseCase(maintenance);
            var result = await useCase.Execute(maintenance.Id);

            result.ShouldNotBeNull();
            result.Id.ShouldBe(maintenance.Id);
            result.ScheduledAt.ShouldBe(maintenance.ScheduledAt);
            result.Vehicle.ShouldNotBeNull();
            result.Vehicle.Id.ShouldBe(vehicle.Id);
            result.IncidentReport.ShouldNotBeNull();
            result.IncidentReport!.Id.ShouldBe(incidentReport.Id);
        }

        [Fact]
        public async Task Success_Without_IncidentReport()
        {
            var vehicle = VehicleBuilder.Build(1);
            var maintenance = MaintenanceBuilder.Build(1, vehicleId: vehicle.Id)
                .WithVehicle(vehicle);

            var useCase = CreateUseCase(maintenance);
            var result = await useCase.Execute(maintenance.Id);

            result.ShouldNotBeNull();
            result.IncidentReport.ShouldBeNull();
        }

        [Fact]
        public async Task Error_Maintenance_Not_Found()
        {
            var useCase = CreateUseCase(maintenance: null);
            var act = async () => await useCase.Execute(999);

            var result = await act.ShouldThrowAsync<NotFoundException>();
            result.Message.ShouldBe(ResourceErrorMessages.MAINTENANCE_NOT_FOUND);
        }

        private static GetByIdMaintenanceUseCase CreateUseCase(Maintenance? maintenance)
        {
            var repositoryBuilder = new MaintenanceReadOnlyRepositoryBuilder();

            if (maintenance is not null)
                repositoryBuilder.GetById(maintenance.Id, maintenance);

            var repository = repositoryBuilder.Build();

            return new GetByIdMaintenanceUseCase(repository);
        }
    }
}
