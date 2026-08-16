using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.ToMaintenance;
using CommonTestUtilities.Repositories.ToVehicle;
using CommonTestUtilities.Request.ToMaintenance;
using FleetManager.Application.UseCase.ToMaintenance.Register;
using FleetManager.Domain.Entities;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.ToMaintenance.Register
{
    public class RegisterMaintenanceUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var vehicle = VehicleBuilder.Build(1);
            var request = RequestMaintenanceJsonBuilder.Build(vehicle.Id);

            var useCase = CreateUseCase(vehicle);
            var result = await useCase.Execute(request);

            result.ShouldNotBeNull();
            result.VehicleId.ShouldBe(request.VehicleId);
            result.IncidentReportId.ShouldBe(request.IncidentReportId);
            result.ScheduledAt.ShouldBe(request.ScheduledAt);
        }

        [Fact]
        public async Task Success_Without_IncidentReport()
        {
            var vehicle = VehicleBuilder.Build(1);
            var request = RequestMaintenanceJsonBuilder.Build(vehicle.Id, incidentReportId: null);

            var useCase = CreateUseCase(vehicle);
            var result = await useCase.Execute(request);

            result.ShouldNotBeNull();
            result.IncidentReportId.ShouldBeNull();
        }

        [Fact]
        public async Task Error_VehicleId_Zero()
        {
            var request = RequestMaintenanceJsonBuilder.Build(0);

            var useCase = CreateUseCase(vehicle: null);
            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();
            result.GetErrors().ShouldContain(ResourceErrorMessages.VEHICLE_ID_REQUIRED);
        }

        [Fact]
        public async Task Error_IncidentReportId_Negative()
        {
            var vehicle = VehicleBuilder.Build(1);
            var request = RequestMaintenanceJsonBuilder.Build(vehicle.Id, incidentReportId: -1);

            var useCase = CreateUseCase(vehicle);
            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();
            result.GetErrors().ShouldContain(ResourceErrorMessages.INCIDENT_REPORT_ID_INVALID);
        }

        [Fact]
        public async Task Error_ScheduledAt_In_The_Past()
        {
            var vehicle = VehicleBuilder.Build(1);
            var request = RequestMaintenanceJsonBuilder.Build(vehicle.Id, scheduledAt: DateTime.UtcNow.AddDays(-1));

            var useCase = CreateUseCase(vehicle);
            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();
            result.GetErrors().ShouldContain(ResourceErrorMessages.SCHEDULED_AT_CANNOT_BE_IN_THE_PAST);
        }

        [Fact]
        public async Task Error_Vehicle_Not_Found()
        {
            var request = RequestMaintenanceJsonBuilder.Build(999);

            var useCase = CreateUseCase(vehicle: null);
            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<NotFoundException>();
            result.Message.ShouldBe(ResourceErrorMessages.VEHICLE_NOT_FOUND);
        }

        private static RegisterMaintenanceUseCase CreateUseCase(Vehicle? vehicle)
        {
            var writeRepository = new MaintenanceWriteOnlyRepositoryBuilder().Build();

            var vehicleRepositoryBuilder = new VehicleReadOnlyRepositoryBuilder();
            if (vehicle is not null)
                vehicleRepositoryBuilder.GetById(vehicle.Id, vehicle);
            var vehicleRepository = vehicleRepositoryBuilder.Build();

            var unitOfWork = UnitOfWorkBuilder.Build();

            return new RegisterMaintenanceUseCase(writeRepository, vehicleRepository, unitOfWork);
        }
    }
}
