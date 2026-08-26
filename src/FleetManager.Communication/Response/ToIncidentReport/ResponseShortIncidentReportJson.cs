namespace FleetManager.Communication.Response.ToIncidentReport
{
    public class ResponseShortIncidentReportJson
    {
        public long Id { get ; set ;}
        public long ContractId {get ; set ;}
        public long VehicleId {get ; set ;}
        public string IncidentRisk { get ; set ;} = string.Empty;
        public DateTime ReportedAt { get ; set ;}
        public ResponseEnumStatusJson Status { get; set; } = new();


    }
}
