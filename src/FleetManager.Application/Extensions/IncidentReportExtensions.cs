using FleetManager.Communication.Response.ToIncidentReport;
using FleetManager.Domain.Entities;

namespace FleetManager.Application.Extensions
{
    public static class IncidentReportExtensions
    {

        public static ResponseIncidentReportJson ToResponse(this IncidentReport report)
        {
            return new ResponseIncidentReportJson
            {
                Id = report.Id,
                ContractId = report.ContractId,
                Status = report.Status.ToString(),
                IncidentRisk = report.IncidentRisk.ToString(),
                ReportedAt  = report.ReportedAt
            
            };
        }
        public static List<ResponseIncidentReportJson> ToResposne(this List<IncidentReport> reports)
        {
            return [.. reports.Select(ir => ir.ToResponse())];
        }
    }
}
