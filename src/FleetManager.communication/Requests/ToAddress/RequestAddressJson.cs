using System;

namespace FleetManager.communication.Requests.ToAddress;

public class RequestAddressJson
{
    public string Street {get; set;} = string.Empty;
    public string Number {get; set;} = string.Empty;
    public string City {get; set;} = string.Empty;
    public string State {get; set;} = string.Empty;
    public string ZipCode {get; set;} = string.Empty;
}
