

namespace FleetManager.Domain.Entities
{
    public class Client
    {
        public long Id { get; set; }
        public string FirstAndLastName { get; private set; } = string.Empty;
        public string PhoneNumber { get; private set; } = string.Empty;
        public string RG { get; private set; } = string.Empty;
        public string CPF { get; private set; } = string.Empty;
        public string CnhRegisterNumber { get; private set; } = string.Empty;
        public string CnhCategory { get; private set; } = string.Empty;
        public bool IsActive { get; private set; } = true;
        public long AddressId { get; set; }
        public Address Address { get; set; } = default!;

        protected Client() { }

        public Client(string firstAndLastName, string phoneNumber, string rg, string cpf,
                      string cnhRegisterNumber, string cnhCategory, long addressId)
        {
            FirstAndLastName = firstAndLastName;
            PhoneNumber = phoneNumber;
            RG = rg;
            CPF = cpf;
            CnhRegisterNumber = cnhRegisterNumber;
            CnhCategory = cnhCategory;
            AddressId = addressId;
        }

        public void Update(string firstAndLastName, string phoneNumber, string rg, string cnhRegisterNumber, string cnhCategory)
        {
            FirstAndLastName = firstAndLastName;
            PhoneNumber = phoneNumber;
            RG = rg;
            CnhRegisterNumber = cnhRegisterNumber;
            CnhCategory = cnhCategory;
        }

        public void Disable() => IsActive = false;
        public void Activate() => IsActive = true;
    }
}
