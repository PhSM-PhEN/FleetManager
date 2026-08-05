using FleetManager.Domain.Enum;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Domain.Entities
{
    public class Maintenance : AuditableEntity
    {
        public long VehicleId { get; private set; }
        public long? IncidentReportId { get; private set; }
        public DateTime ScheduledAt { get; private set; } 
        public decimal WorkshopBudget { get; private set; }
        public string ProblemDescription { get; private set; } = string.Empty;
        public MaintenanceStatus Status { get; private set; } 
        public Vehicle Vehicle { get; private set; } = default!;
        public IncidentReport IncidentReport { get; private set; } = default!;

        protected Maintenance() { }

        public Maintenance(long vehicleId, long? incidentReportId, decimal workshopBudget, string problemDescription)
        {
            VehicleId = vehicleId;
            IncidentReportId = incidentReportId;
            WorkshopBudget = workshopBudget;
            ProblemDescription = problemDescription;
            ScheduledAt = DateTime.UtcNow.Date;
            Status = MaintenanceStatus.Scheduled;
            RegisterHistoryEvent("Scheuled");
        }

        public void Close()
        {
            if(Status == MaintenanceStatus.Closed)
            {
                throw new BusinessRuleException(ResourceErrorMessages.MAINTENANCE_IS_ALREADY_CLOSED);
            }
            Status = MaintenanceStatus.Closed;
            RegisterHistoryEvent("Closed");
        }
    }
}
