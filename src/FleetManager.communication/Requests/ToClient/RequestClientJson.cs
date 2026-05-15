using System;

namespace FleetManager.communication.Requests.ToClient;

public class RequestClientJson
{
    public string FirstAndLastName {get; set; } = string.Empty;
    public string RG {get ; set ;} = string.Empty;
    public string CPF {get; set;} = string.Empty;
    public string CnhRegisterNumber {get; set;} = string.Empty;
    public string CnhCategory {get; set;} = string.Empty;
    public string PhoneNumber {get ; set ;} = string.Empty;
    public long AddressId {get ; set ;}
}
