namespace FleetManager.Domain.Entities
{
    public class Maintenance : AuditableEntity
    {
        public long VehicleId { get; private set; }
        public long? IncidentReportId { get; private set; }
        public DateTime ScheduledAt { get; private set; } = DateTime.UtcNow.Date;
        public decimal WorkshopBudget { get; private set; }
        public string ProblemDescription { get; private set; } = string.Empty;
        public string Status { get; private set; } = string.Empty;

        protected Maintenance() { }

        public Maintenance(long vehicleId, long? incidentReportId, decimal workshopBudget, string problemDescription)
        {
            VehicleId = vehicleId;
            IncidentReportId = incidentReportId;
            WorkshopBudget = workshopBudget;
            ProblemDescription = problemDescription;
        }

        public void Open()
        {
            Status = "Open";
            RegisterHistoryEvent("Opened");
        }
    }
}
