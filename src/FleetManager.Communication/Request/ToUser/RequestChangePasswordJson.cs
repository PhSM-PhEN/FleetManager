namespace FleetManager.Communication.Request.ToUser
{
    public class RequestChangePasswordJson
    {
        public string OldPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        
    }
}
