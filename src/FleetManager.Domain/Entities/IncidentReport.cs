using FleetManager.Domain.Enum;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Domain.Entities
{
    public class IncidentReport : AuditableEntity
    {
        public long ContractId {get ; private set ;}
        public string Description {get ; private set ;} = string.Empty;
        public IncidentReportStatus Status {get ; private set ;}
        public IncidentRisk IncidentRisk { get; private set; }         
        public DateTime ReportedAt {get ; private set ;} 

        public Contract Contract {get ; private set ;} = default!;

        protected IncidentReport() { }

        public IncidentReport(long contractId, string description, IncidentRisk incidentRisk)
        {
            ContractId = contractId;
            Description = description;
            IncidentRisk = incidentRisk;
            Status = IncidentReportStatus.Reported;
            ReportedAt = DateTime.UtcNow;
            RegisterHistoryEvent("Reported");
        }

        public void Resolve()
        {
            if (Status == IncidentReportStatus.Resolved)
            {
                throw new BusinessRuleException(ResourceErrorMessages.INCIDENT_REPORT_ALREADY_RESOLVED);
            }
            Status = IncidentReportStatus.Resolved;
            RegisterHistoryEvent("Resolved");
        }
    }
}
