namespace FleetManager.Domain.Entities
{
    public class AudiTableEntity
    {
        public long Id { get; set; }
        public long CreatedBy { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public long? UpdateBy { get; private set; } 
        public DateTime? UpdatedAt { get; private set; }

        private string? _lastAction;
        public string? LastAction => _lastAction;

        protected void RegisterHistoryEvent(string action) => _lastAction = action;
        public void ClearHistoryEvent() => _lastAction = null;

        public void SetCreatedBy(long userId)
        {
            CreatedBy = userId;
            CreatedAt = DateTime.UtcNow;
        }
        public void SetUpdatedBy(long userId)
        {
            UpdateBy = userId;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
