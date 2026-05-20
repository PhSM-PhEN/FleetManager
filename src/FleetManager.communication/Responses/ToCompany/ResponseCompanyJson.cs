using System;

namespace FleetManager.communication.Responses.ToCompany;

public class ResponseCompanyJson
{
    public int Id {get ; set ;}
    public string Name { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public long AddressId { get; set; }
}
