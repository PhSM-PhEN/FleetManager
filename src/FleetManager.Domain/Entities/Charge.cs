using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Domain.Entities
{
    public class Charge : AuditableEntity
    {
        public long ContractId {get ; private set ; }
        public string Description {get ; private set ;} = string.Empty;
        public decimal Amount {get ; private set ;}
        public Contract Contract {get ; private set ;} = default!;
      
        protected Charge() { }

        public Charge(long contractId, string description, decimal amount)
        {
            ContractId = contractId;
            Description = description;
            Amount = amount;
        }

        public static Charge ForLateFee(Contract contract)
        {
            if (contract.LateFee is null || contract.LateFee <= 0)
                throw new BusinessRuleException(ResourceErrorMessages.CONTRACT_HAS_NO_LATE_FEE);

            var description = string.Format(ResourceExtensionsMessages.LATE_FEE_CHARGE_DESCRIPTION, contract.DaysLate);

            return new Charge(contract.Id, description, contract.LateFee.Value);
        }
        public static Charge ForExcedMileageFee(Contract contract)
        {
            if(contract.ExcessMileageFee is null || contract.ExcessMileageFee < 0)
                throw new BusinessRuleException("");

            var description = $"Excess mileage {contract.ExcessMileageFee}";
            return new Charge(contract.Id, description ,contract.ExcessMileageFee.Value);
        }
    }
}
