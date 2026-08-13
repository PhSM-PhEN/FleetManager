using FleetManager.Communication.Request.ToMaintenace;
using FleetManager.Exception.ExceptionBase;
using FluentValidation;

namespace FleetManager.Application.UseCase.ToMaintenance
{
    public class MaintenanceValidator : AbstractValidator<RequestMaintenanceJson>
    {
        public MaintenanceValidator()
        {
            RuleFor(m => m.VehicleId).GreaterThan(0).WithMessage(ResourceErrorMessages.VEHICLE_ID_REQUIRED);
            RuleFor(m => m.IncidentReportId).GreaterThanOrEqualTo(0).WithMessage("Id invalid");
            RuleFor(m => m.ScheduledAt).NotNull().GreaterThanOrEqualTo(DateTime.UtcNow).WithMessage("Data nao desve estar no passado");
        }
    }
}
