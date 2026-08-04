namespace FleetManager.Domain.Entities
{
    public class Charge : AudiTableEntity
    {
        public long ContractId {get ; private set ; }
        public string Description {get ; private set ;} = string.Empty;
        public decimal Amount {get ; private set ;}
        public Contract Contract {get ; set ;} = default!;
    }
}
