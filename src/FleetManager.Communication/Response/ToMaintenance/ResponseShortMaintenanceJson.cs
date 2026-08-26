namespace FleetManager.Communication.Response.ToMaintenance
{
    public class ResponseShortMaintenanceJson
    {
        public long Id {get ; set ;}
        public long VehicleId { get;  set; }
        public string ServiceCenter {get ; set ;} = string.Empty;
        public long? IncidentReportId { get;  set; }
        public DateTime ScheduledAt { get;  set; }
        public ResponseEnumStatusJson Status { get; set; } = new();
    }
}
