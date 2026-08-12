namespace FleetManager.Communication.Request.ToMaintenace
{
    public class RequestMaintenanceJson
    {
        public long VehicleId { get;  set; }
        public long? IncidentReportId { get;  set; }
        public decimal WorkshopBudget { get;  set; }
        public string ProblemDescription { get;  set; } = string.Empty;

    }
}
