namespace FleetManager.Domain.EnumExtensions
{
    public static class TenantStatusExtension
    {
        public static string TenantStatusToString(this TenantStatus tenantStatus)
        {
            return tenantStatus switch
            {
               TenantStatus.Available => "Available" ,
               TenantStatus.Rented => "Rented",
               TenantStatus.Delinquent => "Delinquent",
               TenantStatus.Deactivate => "Deactivate",
               _ => throw new ArgumentOutOfRangeException(nameof(tenantStatus), tenantStatus, null)
            };
        }
        

    }
}
