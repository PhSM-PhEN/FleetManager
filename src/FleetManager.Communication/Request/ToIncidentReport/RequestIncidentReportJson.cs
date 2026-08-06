namespace FleetManager.Communication.Request.ToIncidentReport
{
    public class RequestIncidentReportJson
    {
        public long ContractId { get ;  set ;}
        public long VehicleId {get ; set ;}
        public string Description { get ; set ;} = string.Empty;
        public string IncidentRisk {get ; set ;} = string.Empty;
        
    }
}
