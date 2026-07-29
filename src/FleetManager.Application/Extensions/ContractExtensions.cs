using FleetManager.Communication.Response.ToContract;
using FleetManager.Domain.Entities;

namespace FleetManager.Application.Extensions
{
    public static class ContractExtensions
    {
        public static ResponseShortContractJson ToResponse(this Contract contract)
        {
            return new ResponseShortContractJson
            {
                Id = contract.Id,
                PickupDateTime = contract.PickupDateTime,
                ReturnDueDateTime = contract.ReturnDueDateTime,
                TotalDays = contract.TotalDays,
                TotalAmount = contract.TotalAmount
            };
        }
        public static ResponseContractJson ToInfoResponse(this Contract contract)
        {
            return new ResponseContractJson
            {
                Id = contract.Id,
                RentalType =  contract.RentalType.ToString(),
                PickupDateTime = contract.PickupDateTime,
                ReturnDueDateTime = contract.ReturnDueDateTime,
                TotalDays = contract.TotalDays,
                TotalAmount = contract.TotalAmount,
                Tenant = contract.Tenant.ToResponse(),
                Vehicle = contract.Vehicle.ToResponse()

            };
        }
        public static List<ResponseShortContractJson> ToResponse(this List<Contract> contracts)
        {
            return contracts.Select(c => c.ToResponse()).ToList();
        }
    }
}
