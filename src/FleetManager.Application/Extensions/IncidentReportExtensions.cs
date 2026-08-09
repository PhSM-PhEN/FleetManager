using FleetManager.Communication.Response.ToIncidentReport;
using FleetManager.Domain.Entities;

namespace FleetManager.Application.Extensions
{
    public static class IncidentReportExtensions
    {

        public static ResponseShortIncidentReportJson ToResponse(this IncidentReport report)
        {
            return new ResponseShortIncidentReportJson
            {
                Id = report.Id,
                VehicleId = report.VehicleId,
                ContractId = report.ContractId,
                Status = report.Status.ToString(),
                IncidentRisk = report.IncidentRisk.ToString(),
                ReportedAt  = report.ReportedAt
            
            };
        }
        public static ResponseIncidentReportJson ToInfoResponse(this IncidentReport report)
        {
            return new ResponseIncidentReportJson
            {
                Id = report.Id,
                Description = report.Description,
                Status = report.Status.ToString(),
                IncidentRisk = report.IncidentRisk.ToString(),
                ReportedAt = report.ReportedAt,
                Contract = report.Contract.ToInfoResponse(),
                Vehicle = report.Vehicle.ToInfoResponse()

            };
        }
        public static List<ResponseShortIncidentReportJson> ToResponse(this List<IncidentReport> reports)
        {
            return [.. reports.Select(ir => ir.ToResponse())];
        }
    }
}
