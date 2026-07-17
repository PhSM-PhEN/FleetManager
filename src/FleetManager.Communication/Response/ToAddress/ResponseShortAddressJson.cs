namespace FleetManager.Communication.Response.ToAddress
{
    public class ResponseShortAddressJson
    {
        public long Id {get;  set;}
        public string Street {get;  set;} = string.Empty;
        public string ZipCode {get;  set;} = string.Empty;
    }
}
