using FleetManager.Domain.Enum;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Domain.EnumExtensions
{
    public static class VehicleStatusExtensions
    {
        public static string ToStringStatus(this VehicleStatus status)
        {
           return status switch
           {
               VehicleStatus.Available => ResourceExtensionsMessages.AVAILABLE,
               VehicleStatus.Rented => ResourceExtensionsMessages.RENTED,
               VehicleStatus.Maintenance => ResourceExtensionsMessages.MAINTENANCE,
               _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
           } ;
        }
    
    }
}
