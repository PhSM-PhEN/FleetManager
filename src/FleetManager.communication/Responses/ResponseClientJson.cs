using System;

namespace FleetManager.Communication.Responses;

public class ResponseClientJson
{
    public long Id {get ; set ;}
    public string FirstAndLastName {get; set; } = string.Empty;
    public string PhoneNumber {get ; set ;} = string.Empty;
    public string RG {get ; set ;} = string.Empty;
    public string CPF {get; set;} = string.Empty;
    public string CnhRegisterNumber {get; set;} = string.Empty;
    public string CnhCategory {get; set;} = string.Empty;
    public long AddressId {get; set;}
    public ResponseAddressJson Address {get ; set ;} = new();
}
