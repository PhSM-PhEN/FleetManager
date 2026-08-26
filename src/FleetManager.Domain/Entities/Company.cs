using FleetManager.Domain.Enum;

namespace FleetManager.Domain.Entities
{
    public class Company : AuditableEntity
    {
        public string Name { get; private set; } = string.Empty;
        public string Cnpj { get; private set; } = string.Empty;
        public string PhoneNumber { get; private set; } = string.Empty;
        public long AddressId { get; private set; }
        public Address Address { get; internal set; } = default!;
        public CompanyStatus Status { get; private set; }

        protected Company() { }

        public Company(string name, string cnpj, string phoneNumber, long addressId)
        {
            Name = name;
            Cnpj = cnpj;
            PhoneNumber = phoneNumber;
            AddressId = addressId;
            Status = CompanyStatus.Available;
        }

        public void Update(string name, string phoneNumber, long addressId)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            AddressId = addressId;
        }
    }
}
