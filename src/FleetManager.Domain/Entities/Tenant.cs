using FleetManager.Domain.Entities.ValueObjects;

namespace FleetManager.Domain.Entities
{
    public class Tenant : AudiTableEntity
    {
        public string Name { get; private set; } = string.Empty;
        public Cpf Cpf { get; private set; } = default!;
        public string RG { get; private set; } = string.Empty;
        public DriverLicense DriverLicense { get; private set; } = default!;
        public Contact Contact { get; private set; } = default!;
        public bool IsActive { get; private set; } = true;
        public long AddressId { get; private set; }


        public Address Address { get; set; } = default!;

        protected Tenant() { }

        public Tenant(string name, Cpf cpf, string rg, DriverLicense driverLicense, Contact contact, long addressId)
        {
            Name = name;
            Cpf = cpf;
            RG = rg;
            DriverLicense = driverLicense;
            Contact = contact;
            AddressId = addressId;
            
        }

        public void Update(Contact contact, long addressId)
        {
            
            Contact = contact;
            AddressId = addressId;
        }
        public void Disable() => IsActive = false;
        public void Activate() => IsActive = true;
    }
}
