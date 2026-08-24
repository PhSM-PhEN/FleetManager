using FleetManager.Communication.Response.ToMaintenance;
using FleetManager.Domain.Entities;
using FleetManager.Domain.EnumExtensions;

namespace FleetManager.Application.Extensions
{
    public static class MaintenanceExtensions
    {
        public static ResponseShortMaintenanceJson ToResponse(this Maintenance maintenance)
        {
            return new ResponseShortMaintenanceJson
            {   Id = maintenance.Id,
                VehicleId = maintenance.VehicleId,
                IncidentReportId = maintenance.IncidentReportId,
                ServiceCenter = maintenance.ServiceCenter,
                ScheduledAt = maintenance.ScheduledAt
            };
        }
        public static ResponseCloseMaintenanceJson ToCloseResponse(this Maintenance maintenance)
        {
            return new ResponseCloseMaintenanceJson
            {
                Id = maintenance.Id,
                ScheduledAt = maintenance.ScheduledAt,
                ServiceCenter = maintenance.ServiceCenter,
                WorkshopBudget = maintenance.WorkshopBudget,
                ProblemDescription = maintenance.ProblemDescription,
                Status = maintenance.Status.ToMaintenanceString(), 
                VehicleId = maintenance.VehicleId,
                IncidentReportId = maintenance.IncidentReportId,

            };
        }
        public static ResponseMaintenanceJson ToInfoResponse(this Maintenance maintenance)
        {
            return new ResponseMaintenanceJson
            {
                Id = maintenance.Id,
                ScheduledAt = maintenance.ScheduledAt,
                ServiceCenter = maintenance.ServiceCenter,
                WorkshopBudget = maintenance.WorkshopBudget,
                ProblemDescription = maintenance.ProblemDescription,
                Status = maintenance.Status.ToMaintenanceString(), 
                Vehicle = maintenance.Vehicle.ToShortResponse(),
                IncidentReport = maintenance.IncidentReport?.ToResponse()
            };
        }
        public static List<ResponseShortMaintenanceJson> ToResponse(this List<Maintenance> maintenances)
        {
            return maintenances.Select(m => m.ToResponse()).ToList();
        }
    }
}
