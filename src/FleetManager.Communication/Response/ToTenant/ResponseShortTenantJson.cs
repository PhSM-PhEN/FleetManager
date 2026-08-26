namespace FleetManager.Communication.Response.ToTenant
{
    public class ResponseShortTenantJson
    {
        public long Id {get ; set ;}
        public string Name {get ; set ;} = string.Empty;
        public string PhoneNumber {get ; set ;} = string.Empty;
        public ResponseEnumStatusJson Status { get; set; } = new();

    }
}
