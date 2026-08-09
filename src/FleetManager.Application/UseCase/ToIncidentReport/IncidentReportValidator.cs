using FleetManager.Communication.Request.ToIncidentReport;
using FleetManager.Exception.ExceptionBase;
using FluentValidation;

namespace FleetManager.Application.UseCase.ToIncidentReport
{
    public class IncidentReportValidator : AbstractValidator<RequestIncidentReportJson>
    {
        public IncidentReportValidator()
        {
            RuleFor(ir => ir.ContractId).NotEmpty().WithMessage(ResourceErrorMessages.CONTRACT_IS_REQUIRED);
            RuleFor(ir => ir.VehicleId).NotEmpty().WithMessage(ResourceErrorMessages.VEHICLE_IS_REQUIRED);
            RuleFor(ir => ir.Description).NotEmpty().NotNull().WithMessage(ResourceErrorMessages.DESCRIPTION_IS_REQUIRED);
            RuleFor(ir => ir.IncidentRisk).IsInEnum().WithMessage(ResourceErrorMessages.INCIDENT_RISK_INVALID);
        }

    }
}
