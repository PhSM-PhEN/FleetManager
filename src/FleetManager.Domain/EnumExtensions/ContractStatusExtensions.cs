using FleetManager.Domain.Enum;

namespace FleetManager.Domain.EnumExtensions
{
    public static class ContractStatusExtensions
    {
        public static string ContractStatusToString(this ContractStatus status)
        {
            return status switch
            {
                ContractStatus.Reserved => "Reserved",
                ContractStatus.Active => "Acive",
                ContractStatus.Cancelled => "Cancelled",
                ContractStatus.Finished => "Finished",
                ContractStatus.Overdue => "Overdue",
                ContractStatus.Renewed => "Renewed",
                _ => string.Empty
            };
        }
    }
}
