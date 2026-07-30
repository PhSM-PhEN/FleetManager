using FleetManager.Domain.Enum;

namespace FleetManager.Domain.EnumExtensions
{
    public static class RentalTypeExtensions
    {
        public static string RentalTypeToString(this RentalType rentalType)
        {
            return rentalType switch
            {
                RentalType.Daily => "Dayly",
                RentalType.Monthly => "Monthly",
                _=> string.Empty
            };

        }
    }
}
