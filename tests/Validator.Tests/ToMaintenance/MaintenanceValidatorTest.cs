using CommonTestUtilities.Request.ToMaintenance;
using FleetManager.Application.UseCase.ToMaintenance;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace Validator.Tests.ToMaintenance
{
    public class MaintenanceValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new MaintenanceValidator();
            var request = RequestMaintenanceJsonBuilder.Build(1);

            var result = validator.Validate(request);

            result.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Success_IncidentReportId_Null()
        {
            var validator = new MaintenanceValidator();
            var request = RequestMaintenanceJsonBuilder.Build(1, incidentReportId: null);

            var result = validator.Validate(request);

            result.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Success_IncidentReportId_Zero()
        {
            var validator = new MaintenanceValidator();
            var request = RequestMaintenanceJsonBuilder.Build(1, incidentReportId: 0);

            var result = validator.Validate(request);

            result.IsValid.ShouldBeTrue();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Error_VehicleId_Not_Greater_Than_Zero(long vehicleId)
        {
            var validator = new MaintenanceValidator();
            var request = RequestMaintenanceJsonBuilder.Build(vehicleId);

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.VEHICLE_ID_REQUIRED));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Error_IncidentReportId_Negative(long incidentReportId)
        {
            var validator = new MaintenanceValidator();
            var request = RequestMaintenanceJsonBuilder.Build(1, incidentReportId: incidentReportId);

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.INCIDENT_REPORT_ID_INVALID));
        }

        [Fact]
        public void Error_ScheduledAt_In_The_Past()
        {
            var validator = new MaintenanceValidator();
            var request = RequestMaintenanceJsonBuilder.Build(1, scheduledAt: DateTime.UtcNow.AddDays(-1));

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.SCHEDULED_AT_CANNOT_BE_IN_THE_PAST));
        }

        [Fact]
        public void Success_ScheduledAt_Far_In_The_Future()
        {
            var validator = new MaintenanceValidator();
            var request = RequestMaintenanceJsonBuilder.Build(1, scheduledAt: DateTime.UtcNow.AddYears(1));

            var result = validator.Validate(request);

            result.IsValid.ShouldBeTrue();
        }
    }
}
