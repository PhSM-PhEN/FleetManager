using FleetManager.Communication.Response.ToMaintenance;
using FleetManager.Domain.Entities;

namespace FleetManager.Application.Extensions
{
    public static class MaintenanceExtensions
    {
        public static ResponseRegisterMaintenanceJson ToRegisterResponse(this Maintenance maintenance)
        {
            return new ResponseRegisterMaintenanceJson
            {   Id = maintenance.Id,
                VehicleId = maintenance.VehicleId,
                IncidentReportId = maintenance.Id,
                ScheduledAt = maintenance.ScheduledAt
            };
        }
        public static ResposneMaintenanceJson ToInfoResponse(this Maintenance maintenance)
        {
            return new ResposneMaintenanceJson
            {
                Id = maintenance.Id,
                Vehicle = maintenance.Vehicle.ToShortResponse(),
                IncidentReport = maintenance.IncidentReport.ToResponse(),
                ScheduledAt = maintenance.ScheduledAt
            };
        }
    }
}
