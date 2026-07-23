using FleetManager.Communication.Response.ToAddress;

namespace FleetManager.Communication.Response.ToRenant
{
    public class ResponseTenantJson
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string RG { get; set; } = string.Empty;
        public string DriverLicenseNumber { get; set; } = string.Empty;
        public string DriverLicenseCategory { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? Email { get; set; }
        public ResponseAddressJson Address { get; set; } = new();
    }

    
}
