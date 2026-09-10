namespace FleetManager.Communication.Request.ToCompany
{
    public class RequestCompanyUpdateLegalInfoJson
    {
        public string? LegalName { get; set; } = string.Empty;
        public string? StateRegistration { get; set; } = string.Empty;
        public string? MunicipalRegistration { get; set; } = string.Empty;
        public string? PrimaryCnae { get; set; } = string.Empty;

    }
}
