namespace FleetManager.Communication.Request.ToMaintenace
{
    public class RequestClosedMaintenanceJson
    {
        public decimal WorkshopBudget { get; private set; }
        public string ProblemDescription { get; private set; } = string.Empty;
    }
}
