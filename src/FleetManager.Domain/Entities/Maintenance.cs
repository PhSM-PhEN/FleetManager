using FleetManager.Domain.Enum;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Domain.Entities
{
    public class Maintenance : AuditableEntity
    {
        public long VehicleId { get; private set; }
        public long? IncidentReportId { get; private set; }
        public string ServiceCenter { get ; private set ;} = string.Empty;
        public DateTime ScheduledAt { get; private set; } 
        public decimal? WorkshopBudget { get; private set; }
        public string? ProblemDescription { get; private set; } = string.Empty;
        public MaintenanceStatus Status { get; private set; } 
        public Vehicle Vehicle { get; private set; } = default!;
        public IncidentReport? IncidentReport { get; private set; } = default!;

        protected Maintenance() { }

        public Maintenance(long vehicleId, string serviceCenter, IncidentReport? incidentReport, DateTime scheduledAt)
        {
            VehicleId = vehicleId;
            ServiceCenter = serviceCenter;
            IncidentReport = incidentReport;
            ScheduledAt = scheduledAt;
            Status = MaintenanceStatus.Scheduled;
            RegisterHistoryEvent("Scheduled");
        }
        private void Scheduled(DateTime scheduledAt)
        {
            if(scheduledAt < ScheduledAt)
            {
                throw new BusinessRuleException(ResourceErrorMessages.SCHEDULED_AT_CANNOT_BE_IN_THE_PAST);
            }
            ScheduledAt = scheduledAt;
        }

        public void Close(string problemDescription, decimal workshopBudget)
        {
            if(Status == MaintenanceStatus.Closed)
            {
                throw new BusinessRuleException(ResourceErrorMessages.MAINTENANCE_IS_ALREADY_CLOSED);
            }
            ProblemDescription = problemDescription;
            if(workshopBudget <= 0)
            {
                throw new BusinessRuleException(ResourceErrorMessages.MAINTENANCE_WORKSHOP_BUDGET_REQUIRED);
            }
                
            WorkshopBudget = workshopBudget;
            Status = MaintenanceStatus.Closed;
            RegisterHistoryEvent("Closed");
        }
        
    }
}
