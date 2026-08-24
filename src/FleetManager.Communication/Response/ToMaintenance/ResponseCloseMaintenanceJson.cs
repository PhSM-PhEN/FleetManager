namespace FleetManager.Communication.Response.ToMaintenance
{
    public class ResponseCloseMaintenanceJson
    {
        public long Id {get ; set ;}
        public DateTime ScheduledAt { get;  set; }
        public long VehicleId { get; set; }
        public string ServiceCenter {get ; set ;} = string.Empty;
        public long? IncidentReportId { get; set; }
        public decimal? WorkshopBudget {get ; set ;}
        public string? ProblemDescription {get ; set ;} = string.Empty;
        public string Status {get; set ;} = string.Empty;
        
    }
}
