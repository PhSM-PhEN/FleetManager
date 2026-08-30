namespace FleetManager.Communication.Request.ToCompany
{
    public class RequestCompanyJson
    {
        public string LegalName { get; set; } = string.Empty;
        public string TradeName { get; set; } = string.Empty;
        public string Cnpj { get; set; } = string.Empty;
        public string? StateRegistration { get; set; } = string.Empty;
        public string? MunicipalRegistration { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? TaxRegime { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? PrimaryCnae { get; set; } = string.Empty;
        public long AddressId { get; set; }
    }
}