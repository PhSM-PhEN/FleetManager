using FleetManager.Communication.Response.ToContract;
using FleetManager.Communication.Response.ToVehicle;

namespace FleetManager.Communication.Response.ToIncidentReport
{
    public class ResponseIncidentReportJson
    {
        public long Id { get; set; }
        public string Description { get; set; } = string.Empty; 
        public ResponseContractJson Contract { get; set; } = new();
        public ResponseVehicleJson Vehicle { get; set; } = new();
        public string Status { get; set; } = string.Empty;
        public string IncidentRisk { get; set; } = string.Empty;
        public DateTime ReportedAt { get; set; }
    }
}
