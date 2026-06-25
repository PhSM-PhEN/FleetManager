namespace FleetManager.Communication.Responses
{
    public class ResponseCompanyJson
    {
        public long Id {get ; set ;}
        public string Name { get; set; } = string.Empty;
        public string Cnpj { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public long AddressId { get; set; } 
        public ResponseAddressJson Address {get ; set ;} = new();
    }
}
