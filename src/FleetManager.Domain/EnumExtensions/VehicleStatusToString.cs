using FleetManager.Domain.Enum;

namespace FleetManager.Domain.EnumExtensions
{
    public static class VehicleStatusExtensions
    {
        public static string VehicleStatusToString(this VehicleStatus status)
        {
           return status switch
           {
               VehicleStatus.Available => "Available",
               VehicleStatus.Rented => "Rented",
               VehicleStatus.Maintenance => "Maintenance",
               _ => string.Empty,
           } ;
        }
    
    }
}
