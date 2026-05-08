using System;

namespace FleetManager.communication.Resposnes.ToAddress;

public class ResponseShortAddressJson
{
    public long Id {get ; set ;}
    public string Street {get; set;} = string.Empty;
    public string City {get ; set ;} = string.Empty;
}
