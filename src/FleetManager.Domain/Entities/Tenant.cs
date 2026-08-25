using FleetManager.Domain.Entities.ValueObjects;
using FleetManager.Domain.Enum;
using FleetManager.Domain.EnumExtensions;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Domain.Entities
{
    public class Tenant : AuditableEntity
    {
        public string Name { get; private set; } = string.Empty;
        public Cpf Cpf { get; private set; } = default!;
        public string RG { get; private set; } = string.Empty;
        public DriverLicense DriverLicense { get; private set; } = default!;
        public Contact Contact { get; private set; } = default!;
        public TenantStatus Status { get; private set; }

        public long AddressId { get; private set; }


        public Address Address { get; internal set; } = default!;
        public TenantStatus GetStatus {get => Status ;}
        protected Tenant() { }

        public Tenant(string name, Cpf cpf, string rg, DriverLicense driverLicense, Contact contact, long addressId)
        {
            Name = name;
            Cpf = cpf;
            RG = rg;
            DriverLicense = driverLicense;
            Contact = contact;
            AddressId = addressId;
            Status = TenantStatus.Available;
            
            
        }

        public void Update(Contact contact, long addressId)
        {
            
            Contact = contact;
            AddressId = addressId;
        }
        public void Deactivate()
        {
            if (Status == TenantStatus.Deactivate)
                throw new BusinessRuleException(ResourceErrorMessages.TENANT_ALREADY_DEACTIVATED);

            Status = TenantStatus.Deactivate;
            RegisterHistoryEvent("Disabled");
        }

        public void Activate()
        {
            if (Status == TenantStatus.Available)
                throw new BusinessRuleException(ResourceErrorMessages.TENANT_ALREADY_ACTIVE);

            Status = TenantStatus.Available;
            RegisterHistoryEvent("Activated");
        }
    }
}
