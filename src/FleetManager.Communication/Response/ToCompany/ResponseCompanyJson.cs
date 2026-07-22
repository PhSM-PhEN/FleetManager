using FleetManager.Communication.Response.ToAddress;

namespace FleetManager.Communication.Response.ToCompany
{
    public class ResponseCompanyJson
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Cnpj { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public ResponseAddressJson Address { get; set; } = new();
    }
}