using FleetManager.Communication.Response.ToMaintenance;
using FleetManager.Domain.Entities;

namespace FleetManager.Application.Extensions
{
    public static class MaintenanceExtensions
    {
        public static ResponseShortMaintenanceJson ToResponse(this Maintenance maintenance)
        {
            return new ResponseShortMaintenanceJson
            {   Id = maintenance.Id,
                VehicleId = maintenance.VehicleId,
                IncidentReportId = maintenance.IncidentReport?.Id,
                ScheduledAt = maintenance.ScheduledAt
            };
        }
        public static ResponseMaintenanceJson ToInfoResponse(this Maintenance maintenance)
        {
            return new ResponseMaintenanceJson
            {
                Id = maintenance.Id,
                ScheduledAt = maintenance.ScheduledAt,
                WorkshopBudget = maintenance.WorkshopBudget,
                ProblemDescription = maintenance.ProblemDescription,
                Status = maintenance.Status.ToString(), // adicionar o metodo de extençao para o maintence status depois 
                Vehicle = maintenance.Vehicle.ToShortResponse(),
                IncidentReport = maintenance.IncidentReport?.ToResponse(),
                

            };
        }
        public static List<ResponseShortMaintenanceJson> ToResponse(this List<Maintenance> maintenances)
        {
            return maintenances.Select(m => m.ToResponse()).ToList();
        }
    }
}
