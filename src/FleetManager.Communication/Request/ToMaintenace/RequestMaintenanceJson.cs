namespace FleetManager.Communication.Request.ToMaintenace
{
    public class RequestMaintenanceJson
    {
        public long VehicleId { get; private set; }
        public long? IncidentReportId { get; private set; }
        public DateTime ScheduledAt { get; private set; } 
       

    }
}
