namespace FleetManager.Domain.Entities
{
    public class Company 
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Cnpj { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public long AddressId { get; set; }
        public Address Address { get; set; } = default!;


    }
}
