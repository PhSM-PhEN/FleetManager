namespace FleetManager.Domain.Entities
{
    public class Maintenacem : AudiTableEntity
    {
        public long VehicleId { get ; private set ;}
        public long? IncidentReportId {get ; private set ;}
        public DateTime SheduledAt { get ; private set ;} = DateTime.UtcNow.Date;
        public decimal WorkshopBudget {get ; private set ; }
        public string ProblemDescription {get ; private set ;} = string.Empty;
        public string Status {get ; private set ;} = string.Empty;

        protected Maintenacem() {}
        public Maintenacem(long vehicleId, long? incidentReportId, decimal workshopBudget, string problemDescription)
        {
            VehicleId = vehicleId;
            IncidentReportId = incidentReportId;
            WorkshopBudget = workshopBudget;
            ProblemDescription = problemDescription;
            
        }
        public void Open ()
        {
            Status = "Open";
        }
    }
}
