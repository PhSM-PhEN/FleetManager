using FleetManager.Communication.Response.ToContract;
using FleetManager.Domain.Entities;

namespace FleetManager.Application.Extensions
{
    public static class ContractExtensions
    {
        public static ResponseContractJson ToInfoResponse(this Contract contract)
        {
            return new ResponseContractJson
            {
                Id = contract.Id,
                RentalType =  contract.RentalType.ToString(),
                PickupDateTime = contract.PickupDateTime,
                ReturnDueDateTime = contract.ReturnDueDateTime,
                TotalDays = contract.TotalDays,
                MileageAllowance = contract.SnapshotMileageAllowed,
                BaseRentalAmount = contract.BaseRentalAmount,
                AdditionalKilometersAmount = contract.AdditionalKilometersAmount,
                TotalAmount = contract.TotalAmount,
                Tenant = contract.Tenant.ToResponse(),
                Vehicle = contract.Vehicle.ToResponse()

            };
        }
    }
}
