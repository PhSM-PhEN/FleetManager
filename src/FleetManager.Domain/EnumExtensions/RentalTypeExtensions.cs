using FleetManager.Domain.Enum;
using FleetManager.Exception.ExceptionBase;
using System.Net.NetworkInformation;

namespace FleetManager.Domain.EnumExtensions
{
    public static class RentalTypeExtensions
    {
        public static string ToStringStatus(this RentalType status)
        {
            return status switch
            {
                RentalType.Daily => ResourceExtensionsMessages.DAILY,
                RentalType.Monthly => ResourceExtensionsMessages.MONTHLY,
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
            };

        }
    }
}
