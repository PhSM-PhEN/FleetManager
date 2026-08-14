namespace FleetManager.Communication.Request.ToMaintenance
{
    public class RequestClosedMaintenanceJson
    {
        public decimal WorkshopBudget { get;  set; }
        public string ProblemDescription { get;  set; } = string.Empty;
    }
}
