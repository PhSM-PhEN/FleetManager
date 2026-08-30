using FleetManager.Domain.Enum;
using FleetManager.Exception.ExceptionBase;
using System.Data;

namespace FleetManager.Domain.Entities
{
    public class Charge : AuditableEntity
    {
        public long ContractId {get ; private set ; }
        public string Description {get ; private set ;} = string.Empty;
        public decimal Amount {get ; private set ;}
        public ChargeStatus Status {get ; private set ; }
        public Contract Contract {get ; private set ;} = default!;
      
        protected Charge() { }

        public Charge(long contractId, string description, decimal amount)
        {
            ContractId = contractId;
            Description = description;
            Amount = amount;
            Status = ChargeStatus.Pending;
        }
        public void MarkAsPaid()
        {
            Status = ChargeStatus.Paid;
        }
        public void MarkAsOverdue()
        {
            Status = ChargeStatus.Overdue;
        }
        public void MarkAsCancelled()
        {
            Status = ChargeStatus.Cancelled;
        }
       
        public static Charge ForLateFee(Contract contract)
        {
            if (contract.LateFee is null || contract.LateFee <= 0)
                throw new BusinessRuleException(ResourceErrorMessages.CONTRACT_HAS_NO_LATE_FEE);

            var description = string.Format(ResourceExtensionsMessages.LATE_FEE_CHARGE_DESCRIPTION, contract.DaysLate);
            return new Charge(contract.Id, description, contract.LateFee.Value);
        }
        public static Charge ForContractStart(Contract contract)
        {
            if(contract.GetStatus != ContractStatus.Active)
            {
                throw new BusinessRuleException(ResourceErrorMessages.CONTRACT_NOT_ACTIVE);

            }
            var description = string.Format(ResourceExtensionsMessages.PENDING, contract.TotalAmount);
            return new Charge(contract.Id, description, contract.TotalAmount);

        }
        public static Charge ForExceededMileageFee(Contract contract)
        {
            if (contract.ExcessMileageFee is null || contract.ExcessMileageFee <= 0)
                throw new BusinessRuleException(ResourceErrorMessages.CONTRACT_HAS_NO_EXCESS_MILEAGE_FEE);

            var excessMileage = Math.Max(0, (contract.FinalMileage ?? 0) - contract.StartMileage - contract.MileageContracted);
            var description = string.Format(ResourceExtensionsMessages.EXCESS_MILEAGE_CHARGE_DESCRIPTION, excessMileage);

            return new Charge(contract.Id, description, contract.ExcessMileageFee.Value);
        }
    }
}
