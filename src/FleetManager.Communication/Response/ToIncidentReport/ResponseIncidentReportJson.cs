namespace FleetManager.Communication.Response.ToIncidentReport
{
    public class ResponseIncidentReportJson
    {
        public long Id { get ; set ;}
        public long ContractId {get ; set ;}
        public long VehicleId {get ; set ;}
        public string Status { get ; set ;} = string.Empty;
        public string IncidentRisk { get ; set ;} = string.Empty;
        public DateTime ReportedAt { get ; set ;}
        

    }
}
