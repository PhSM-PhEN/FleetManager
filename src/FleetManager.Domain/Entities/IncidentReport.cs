namespace FleetManager.Domain.Entities
{
    public class IncidentReport : AudiTableEntity
    {
        public long ContractId {get ; private set ;}
        public string Description {get ; private set ;} = string.Empty;
        public string Status {get ; private set ;} = string.Empty;
        public DateTime ReportedAt {get ; private set ;} 

        public Contract Contract {get ; private set ;} = default!;
    }
}
