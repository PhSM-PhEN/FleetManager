namespace FleetManager.Domain.Entities
{
    public class HistoryLog
    {
        public long Id { get; private set; }
        public string EntityName { get; private set; } = string.Empty;
        public long EntityId { get; private set; }
        public string Action { get; private set; } = string.Empty;
        public long PerformedBy { get; private set; }
        public string PerformedByName { get; private set; } = string.Empty;
        public DateTime PerformedAt { get; private set; }

        protected HistoryLog() { }

        public HistoryLog(string entityName, long entityId, string action, long performedBy, string performedByName)
        {
            EntityName = entityName;
            EntityId = entityId;
            Action = action;
            PerformedBy = performedBy;
            PerformedByName = performedByName;
            PerformedAt = DateTime.UtcNow;
        }
    }
}
