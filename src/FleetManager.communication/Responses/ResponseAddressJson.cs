using System;

namespace FleetManager.Communication.Responses;

public class ResponseAddressJson
{
    public long Id {get; set;}
    public string Street {get; set;} = string.Empty;
    public string Number {get; set;} = string.Empty;
    public string City {get; set;} = string.Empty;
    public string State {get; set;} = string.Empty;
    public string ZipCode {get; set;} = string.Empty;
}
