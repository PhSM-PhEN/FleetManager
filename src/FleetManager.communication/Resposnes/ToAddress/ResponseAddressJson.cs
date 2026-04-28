using System;

namespace FleetManager.communication.Resposnes.ToAddress;

public class ResponseAddressJson
{
    public string Street {get; set;} = string.Empty;
    public string Number {get; set;} = string.Empty;
    public string City {get; set;} = string.Empty;
    public string State {get; set;} = string.Empty;
    public string ZipCode {get; set;} = string.Empty;
}
