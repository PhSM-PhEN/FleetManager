using FleetManager.Communication.Responses;
using FleetManager.Domain.Entities;

namespace FleetManager.Application.Extensions
{
    public static class RentalExtensions
    {
        public static ResponseShortRentalJson ToResponse(this Rental rental)
        {
            return new ResponseShortRentalJson
            {
                Id = rental.Id,
                CompanyName = rental.Company.Name,
                ClientName = rental.Client.FirstAndLastName,
                VehicleModel = rental.Vehicle.Model,
                TotalPrice = rental.TotalPrice
            };
        }
        public static List<ResponseShortRentalJson> ToResponse(this List<Rental> rentals)
        {
            return [.. rentals.Select(r => r.ToResponse())];

        }
        public static ResponseRentalJson ToDeTailResponse(this Rental rental)
        {
            return new ResponseRentalJson
            {
                Company = rental.Company.ToResponse(),
                Client = rental.Client.ToDetailResponse(),
                Vehicle = rental.Vehicle.ToDetailResponse(),
                RentalPlan = rental.RentalPlan.ToDetailResponse()

            };
        }
    }
}
