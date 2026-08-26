using FleetManager.Communication.Response;
using FleetManager.Communication.Response.ToIncidentReport;
using FleetManager.Domain.Entities;
using FleetManager.Domain.EnumExtensions;

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
                IncidentRisk = report.IncidentRisk.ToStringStatus(),
                ReportedAt = report.ReportedAt,
                Status = new ResponseEnumStatusJson
                {
                    Id = (int)report.Status,
                    Label = report.Status.ToStringStatus(),
                }

            };
        }
        public static ResponseIncidentReportJson ToInfoResponse(this IncidentReport report)
        {
            return new ResponseIncidentReportJson
            {
                Id = report.Id,
                Description = report.Description,
                IncidentRisk = report.IncidentRisk.ToStringStatus(),
                ReportedAt = report.ReportedAt,
                Contract = report.Contract.ToInfoResponse(),
                Vehicle = report.Vehicle.ToInfoResponse(),
                Status = new ResponseEnumStatusJson
                {
                    Id = (int)report.Status,
                    Label = report.Status.ToStringStatus(),
                }


            };
        }
        public static List<ResponseShortIncidentReportJson> ToResponse(this List<IncidentReport> reports)
        {
            return [.. reports.Select(ir => ir.ToResponse())];
        }
    }
}
