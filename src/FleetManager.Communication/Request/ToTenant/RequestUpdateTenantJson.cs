namespace FleetManager.Communication.Request.ToTenant
{
    public class RequestUpdateTenantJson
    {
        public string PhoneNumber { get; set; } = string.Empty;
        public string? Email { get; set; }
        public long AddressId { get; set; }
    }
}
