namespace FleetManager.Communication.Request.ToMaintenance
{
    public class RequestMaintenanceJson
    {
        public long VehicleId { get;  set; }
        public string ServiceCenter {get ; set ;} = string.Empty;
        public long? IncidentReportId { get;  set; }
        public DateTime ScheduledAt { get;  set; } 
       

    }
}
