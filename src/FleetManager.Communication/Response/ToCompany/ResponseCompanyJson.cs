using FleetManager.Communication.Response.ToAddress;

namespace FleetManager.Communication.Response.ToCompany
{
    public class ResponseCompanyJson
    {
        public long Id { get; set; }
        public string? LegalName { get; set; } = string.Empty;
        public string TradeName { get; set; } = string.Empty;
        public string Cnpj { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? PrimaryCnae {  get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public ResponseAddressJson Address { get; set; } = new();
        public ResponseEnumStatusJson Status { get; set; } = new();
    }
}
