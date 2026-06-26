using FleetManager.Communication.Responses;
using FleetManager.Communication.ToEnums;
using FleetManager.Domain.Entities;

namespace FleetManager.Application.Extensions
{
    public static class RentalPlanExtensions
    {
        public static ResponseRentalPlanJson ToDetailResponse(this RentalPlan rentalPlan)
        {
            return new ResponseRentalPlanJson
            {
                Id = rentalPlan.Id,
                Name = rentalPlan.Name,
                Mode = (Communication.ToEnums.RentalMode)rentalPlan.Mode,
                Transmission = (Communication.ToEnums.TransmissionType)rentalPlan.Transmission,
                PriceRental = rentalPlan.PriceRental,
                PricePerKm = rentalPlan.PricePerKm,

            };
        }
        public static ResponseShortRentalPlansJson ToResponse(this RentalPlan rentalPlan)
        {
            return new ResponseShortRentalPlansJson
            {
                Id = rentalPlan.Id,
                Name = rentalPlan.Name,
                RentalMode = (RentalMode)rentalPlan.Mode,
                Transmission = (TransmissionType)rentalPlan.Transmission

            };
        }

        public static List<ResponseShortRentalPlansJson> ToResponse(this List<RentalPlan> rentalPlans)
        {
            return [.. rentalPlans.Select(rp => rp.ToResponse())];
        }

    }
}