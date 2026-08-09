using FleetManager.Domain.Enum;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Domain.EnumExtensions
{
    public static class RentalTypeExtensions
    {
        public static string RentalTypeToString(this RentalType rentalType)
        {
            return rentalType switch
            {
                RentalType.Daily => ResourceExtensionsMessages.DAILY,
                RentalType.Monthly => ResourceExtensionsMessages.MONTHLY,
                _=> string.Empty
            };

        }
    }
}
