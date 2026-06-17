namespace FleetManager.Communication.Requests;

public class RequestCompanyJson
{
    public string Name { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public long AddressId { get; set; }
}
