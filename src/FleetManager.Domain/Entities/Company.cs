using FleetManager.Domain.Entities.ValueObjects;
using FleetManager.Domain.Enum;

namespace FleetManager.Domain.Entities
{
    public class Company : AuditableEntity
    {
        public long AddressId { get; private set; }
        public string? LegalName { get; private set; } = string.Empty;
        public string TradeName { get; private set; } = string.Empty;
        public string Cnpj { get; private set; } = string.Empty;
        public string? StateRegistration { get; private set; } = string.Empty;
        public string? MunicipalRegistration { get; private set; } = string.Empty;
        public TaxRegimeEnum? TaxRegime { get; private set; }
        public string? PrimaryCnae { get; private set; }
        public Contact Contact { get; private set; } = default!;
        public Address Address { get; internal set; } = default!;
        public CompanyStatus Status { get; private set; }
    

        protected Company() { }

        public Company(string? legalName, string tradeName, string? stateRegistration, string? municipalRegistration,string? primaryCnae, TaxRegimeEnum? taxRegime, string cnpj, Contact contact, long addressId)
        {
            LegalName = legalName;
            TradeName = tradeName;
            Cnpj = cnpj;
            StateRegistration = stateRegistration;
            MunicipalRegistration = municipalRegistration;
            PrimaryCnae = primaryCnae;
            Contact = contact;
            AddressId = addressId;
            TaxRegime = taxRegime;
            Status = CompanyStatus.Available;
        }

        public void UpdateAddress(long addressId)
        {
            AddressId = addressId;
        }
        public void UpdateContact(Contact contact)
        {
            Contact = contact;
        }
        public void UpdateLegalInfo( string? stateRegistration, string? municipalRegistration, string primaryCnae)
        {
            StateRegistration = stateRegistration;
            MunicipalRegistration = municipalRegistration;
            PrimaryCnae = primaryCnae;
        }

        public void UpdateTaxRegime(TaxRegimeEnum taxRegime)
        {
            TaxRegime = taxRegime;
        }
        public void Activate()
        {
            Status = CompanyStatus.Available;
        }

        public void Deactivate()
        {
            Status = CompanyStatus.Unavailable;
        }
    }
}
