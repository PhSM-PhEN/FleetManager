using FleetManager.Communication.Request.ToIncidentReport;
using FleetManager.Exception.ExceptionBase;
using FluentValidation;

namespace FleetManager.Application.UseCase.ToIncidentReport
{
    public class IncidentReportValidator : AbstractValidator<RequestIncidentReportJson>
    {
        public IncidentReportValidator()
        {
            RuleFor(ir => ir.ContractId).NotEmpty().WithMessage("Contract is required");
            RuleFor(ir => ir.VehicleId).NotEmpty().WithMessage("Vehicle is required.");
            RuleFor(ir => ir.Description).NotEmpty().NotNull().WithMessage("Description is required");
            RuleFor(ir => ir.IncidentRisk).IsInEnum().WithMessage("incident risk invalid");

        }

    }
}
