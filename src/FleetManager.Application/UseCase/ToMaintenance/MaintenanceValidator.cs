using FleetManager.Communication.Request.ToMaintenance;
using FleetManager.Exception.ExceptionBase;
using FluentValidation;

namespace FleetManager.Application.UseCase.ToMaintenance
{
    public class MaintenanceValidator : AbstractValidator<RequestMaintenanceJson>
    {
        public MaintenanceValidator()
        {
            RuleFor(m => m.VehicleId).GreaterThan(0).WithMessage(ResourceErrorMessages.VEHICLE_ID_REQUIRED);
            RuleFor(m => m.IncidentReportId).GreaterThanOrEqualTo(0).WithMessage(ResourceErrorMessages.INCIDENT_REPORT_ID_INVALID);
            RuleFor(m => m.ScheduledAt).NotNull().GreaterThanOrEqualTo(DateTime.UtcNow).WithMessage(ResourceErrorMessages.SCHEDULED_AT_CANNOT_BE_IN_THE_PAST);
            RuleFor(m => m.ServiceCenter).NotEmpty().WithMessage(ResourceErrorMessages.SERVICE_CENTER_IS_REQUIRED);
        }
    }
}
