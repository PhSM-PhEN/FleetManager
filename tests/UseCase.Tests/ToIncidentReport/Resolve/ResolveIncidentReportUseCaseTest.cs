using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.ToIncidentReport;
using CommonTestUtilities.Repositories.ToVehicle;
using FleetManager.Application.UseCase.ToIncidentReport.Resolve;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Enum;
using FleetManager.Domain.Repositories.ToIncidentReport;
using FleetManager.Domain.Repositories.ToVehicle;
using FleetManager.Exception.ExceptionBase;
using Moq;
using Shouldly;

namespace UseCase.Tests.ToIncidentReport.Resolve
{
    public class ResolveIncidentReportUseCaseTest
    {
        [Fact]
        public async Task Success_High_Risk_Resolves_Incident_And_Unblocks_Vehicle()
        {
            var incident = IncidentReportBuilder.Build(id: 1, incidentRisk: IncidentRisk.High);
            var vehicle = VehicleBuilder.Build(id: incident.VehicleId);
            vehicle.BlockForIncident(incident);

            var useCase = CreateUseCase(incident, vehicle);

            await useCase.Execute(incident.Id);

            incident.Status.ShouldBe(IncidentReportStatus.Resolved);
            vehicle.IsBlockedForMaintenance.ShouldBeFalse();
        }

        // Risco Low nunca bloqueou o veículo (ver RegisterIncidentReportUseCaseTest), então
        // resolver esse incidente não pode tentar desbloquear nada.
        [Fact]
        public async Task Success_Low_Risk_Resolves_Incident_Without_Touching_Vehicle()
        {
            var incident = IncidentReportBuilder.Build(id: 1, incidentRisk: IncidentRisk.Low);
            var vehicle = VehicleBuilder.Build(id: incident.VehicleId);

            var useCase = CreateUseCase(incident, vehicle, out var vehicleRepositoryMock);

            await useCase.Execute(incident.Id);

            incident.Status.ShouldBe(IncidentReportStatus.Resolved);
            vehicle.IsBlockedForMaintenance.ShouldBeFalse();
            vehicleRepositoryMock.Verify(v => v.GetById(It.IsAny<long>()), Times.Never);
            vehicleRepositoryMock.Verify(v => v.Update(It.IsAny<Vehicle>()), Times.Never);
        }

        [Fact]
        public async Task Error_Incident_Not_Found()
        {
            var useCase = CreateUseCase(incident: null, vehicle: null);

            var act = async () => await useCase.Execute(999);

            var result = await act.ShouldThrowAsync<NotFoundException>();
            result.Message.ShouldBe(ResourceErrorMessages.INCIDENT_REPORT_NOT_FOUND);
        }

        [Fact]
        public async Task Error_Resolve_Twice()
        {
            var incident = IncidentReportBuilder.Build(id: 1, incidentRisk: IncidentRisk.High);
            var vehicle = VehicleBuilder.Build(id: incident.VehicleId);
            vehicle.BlockForIncident(incident);
            var useCase = CreateUseCase(incident, vehicle);
            await useCase.Execute(incident.Id);

            var act = async () => await useCase.Execute(incident.Id);

            var result = await act.ShouldThrowAsync<BusinessRuleException>();
            result.Message.ShouldBe(ResourceErrorMessages.INCIDENT_REPORT_ALREADY_RESOLVED);
        }

        // O use case só conhece o VehicleId gravado no próprio incidente (FK) — não existe forma
        // de ele mexer em outro veículo por engano. Este teste comprova que o repositório de
        // veículo é consultado exatamente com o Id certo, e nenhum outro.
        [Fact]
        public async Task Resolve_Only_Touches_The_Vehicle_Referenced_By_The_Incident()
        {
            var incidentOnVehicleA = IncidentReportBuilder.Build(id: 1, vehicleId: 10, incidentRisk: IncidentRisk.High);
            var vehicleA = VehicleBuilder.Build(id: 10);
            vehicleA.BlockForIncident(incidentOnVehicleA);

            var useCase = CreateUseCase(incidentOnVehicleA, vehicleA, out var vehicleRepositoryMock);

            await useCase.Execute(incidentOnVehicleA.Id);

            vehicleRepositoryMock.Verify(v => v.GetById(10), Times.Once);
            vehicleRepositoryMock.Verify(v => v.GetById(It.Is<long>(id => id != 10)), Times.Never);
        }

        private static ResolveIncidentReportUseCase CreateUseCase(IncidentReport? incident, Vehicle? vehicle)
        {
            return CreateUseCase(incident, vehicle, out _);
        }

        private static ResolveIncidentReportUseCase CreateUseCase(IncidentReport? incident, Vehicle? vehicle,
            out Mock<IVehicleWriteOnlyRepository> vehicleRepositoryMock)
        {
            var incidentRepositoryBuilder = new IncidentReportWriteOnlyRepositoryBuilder();
            if (incident is not null)
            {
                incidentRepositoryBuilder.GetById(incident.Id, incident);
                incidentRepositoryBuilder.Update(incident);
            }
            else
            {
                incidentRepositoryBuilder.GetById(999, null);
            }

            var vehicleRepositoryBuilder = new VehicleWriteOnlyRepositoryBuilder();
            if (vehicle is not null)
            {
                vehicleRepositoryBuilder.GetById(vehicle.Id, vehicle);
                vehicleRepositoryBuilder.Update(vehicle);
            }

            IIncidentReportWriteOnlyRepository incidentRepository = incidentRepositoryBuilder.Build();
            var vehicleRepository = vehicleRepositoryBuilder.Build();
            vehicleRepositoryMock = Mock.Get(vehicleRepository);
            var unitOfWork = UnitOfWorkBuilder.Build();

            return new ResolveIncidentReportUseCase(incidentRepository, vehicleRepository, unitOfWork);
        }
    }
}
