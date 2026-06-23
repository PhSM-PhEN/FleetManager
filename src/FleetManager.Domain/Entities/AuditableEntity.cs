namespace FleetManager.Domain.Entities;

public abstract class AuditableEntity
{
    public long Id { get; set; }
    public long CreatedBy { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public long? UpdatedBy { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public void SetCreatedBy(long userId)
    {
        CreatedBy = userId;
        CreatedAt = DateTime.UtcNow;
    }

    public void SetUpdatedBy(long userId)
    {
        UpdatedBy = userId;
        UpdatedAt = DateTime.UtcNow;
    }
}
