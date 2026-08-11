using FleetManager.Communication.Response.ToContract;
using FleetManager.Domain.Entities;
using FleetManager.Domain.EnumExtensions;

namespace FleetManager.Application.Extensions
{
    public static class ContractExtensions
    {
        public static ResponseShortContractJson ToResponse(this Contract contract)
        {
            return new ResponseShortContractJson
            {
                Id = contract.Id,
                ContractStatus = contract.ContractStatus.ContractStatusToString(),
                PickupDateTime = contract.PickupDateTime,
                ReturnDueDateTime = contract.ReturnDueDateTime,
                TotalDays = contract.TotalDays,
                TotalAmount = contract.TotalAmount,


            };
        }
        public static ResponseContractJson ToInfoResponse(this Contract contract)
        {
            return new ResponseContractJson
            {
                Id = contract.Id,
                RentalType = contract.RentalType.RentalTypeToString(),
                ContractStatus = contract.ContractStatus.ContractStatusToString(),
                PickupDateTime = contract.PickupDateTime,
                ReturnDueDateTime = contract.ReturnDueDateTime,
                ActualReturnDateTime = contract.ActualReturnDateTime,
                TotalDays = contract.TotalDays,
                StartMileage = contract.StartMileage,
                EndMileage = contract.EndMileage,
                FinalMileage = contract.FinalMileage,
                ExcessMileageFee = contract.ExcessMileageFee,
                MileageContracted = contract.MileageContracted,
                SnapshotPriceDailyRate = contract.SnapshotPriceDailyRate,
                SnapshotPriceMonthlyRate = contract.SnapshotPriceMonthlyRate,
                SnapshotPricePerExtraMileage = contract.SnapshotPricePerExtraMileage,
                TotalAmount = contract.TotalAmount,
                Tenant = contract.Tenant.ToInfoResponse(),
                Vehicle = contract.Vehicle.ToResponse(),




            };
        }
        public static ResponseCompleteContractJson ToCompleteResponse(this Contract contract)
        {
            return new ResponseCompleteContractJson
            {
                ContractId = contract.Id,
                ActualReturnDateTime = contract.ActualReturnDateTime!.Value,
                FinalMileage = contract.FinalMileage!.Value,
                ExcessMileageFee = contract.ExcessMileageFee,
                DaysLate = contract.DaysLate,
                LateFee = contract.LateFee,
                TotalCharged = (contract.ExcessMileageFee ?? 0) + (contract.LateFee ?? 0)
            };
        }
        public static List<ResponseShortContractJson> ToResponse(this List<Contract> contracts)
        {
            return contracts.Select(c => c.ToResponse()).ToList();
        }
    }
}
