using FleetManager.Communication.Response;
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
                PickupDateTime = contract.PickupDateTime,
                ReturnDueDateTime = contract.ReturnDueDateTime,
                TotalDays = contract.TotalDays,
                TotalAmount = contract.TotalAmount,
                Status = new ResponseEnumStatusJson
                {
                    Id = (int)contract.Status,
                    Label = contract.Status.ToStringStatus()
                }



            };
        }
        public static ResponseContractJson ToInfoResponse(this Contract contract)
        {
            return new ResponseContractJson
            {
                Id = contract.Id,
                RentalType = contract.RentalType.ToStringStatus(),
               
                PickupDateTime = contract.PickupDateTime,
                ReturnDueDateTime = contract.ReturnDueDateTime,
                ActualReturnDateTime = contract.ActualReturnDateTime,
                TotalDays = contract.TotalDays,
                StartMileage = contract.StartMileage,
                ExpectedEndMileage = contract.ExpectedEndMileage,
                FinalMileage = contract.FinalMileage,
                ExcessMileageFee = contract.ExcessMileageFee,
                MileageContracted = contract.MileageContracted,
                SnapshotPriceDailyRate = contract.SnapshotPriceDailyRate,
                SnapshotPriceMonthlyRate = contract.SnapshotPriceMonthlyRate,
                SnapshotPricePerExtraMileage = contract.SnapshotPricePerExtraMileage,
                TotalAmount = contract.TotalAmount,
                Tenant = contract.Tenant.ToInfoResponse(),
                Vehicle = contract.Vehicle.ToResponse(),
                Status = new ResponseEnumStatusJson
                {
                    Id = (int)contract.Status,
                    Label = contract.Status.ToStringStatus()
                },




            };
        }
        public static ResponseFinishUpContractJson ToFinishUpResponse(this Contract contract)
        {
            return new ResponseFinishUpContractJson
            {
                ContractId = contract.Id,
                ActualReturnDateTime = contract.ActualReturnDateTime!.Value,
                FinalMileage = contract.FinalMileage!.Value,
                ExcessMileageFee = contract.ExcessMileageFee,
                DaysLate = contract.DaysLate,
                LateFee = contract.LateFee,
                TotalCharged = (contract.ExcessMileageFee ?? 0) + (contract.LateFee ?? 0),
                 Status = new ResponseEnumStatusJson
                 {
                     Id = (int)contract.Status,
                     Label = contract.Status.ToStringStatus()
                 }
            };
        }
        public static List<ResponseShortContractJson> ToResponse(this List<Contract> contracts)
        {
            return [.. contracts.Select(c => c.ToResponse())];
        }
    }
}
