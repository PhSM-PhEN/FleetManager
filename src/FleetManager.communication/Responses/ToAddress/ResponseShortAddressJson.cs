using System;

namespace FleetManager.communication.Responses.ToAddress;

public class ResponseShortAddressJson
{
    public long Id {get ; set ;}
    public string Street {get; set;} = string.Empty;
    public string City {get ; set ;} = string.Empty;
}
