namespace FleetManager.Domain.Entities
{
    public class Company : AuditableEntity
    {
        
        public string Name { get; private set; } = string.Empty;
        public string Cnpj { get; private set; } = string.Empty;
        public string PhoneNumber { get; private set; } = string.Empty;
        public long AddressId { get; set; }
        public Address Address { get; set; } = default!;

        protected Company() { }

        public Company(string name, string cnpj, string phoneNumber, long addressId)
        {
            Name = name;
            Cnpj = cnpj;
            PhoneNumber = phoneNumber;
            AddressId = addressId;
        }

        public void Update(string name, string phoneNumber, long addressId)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            AddressId = addressId;
        }
    }
}
