namespace FleetManager.Domain.Entities.ValueObjects
{
    public class Contact
    {
        public string PhoneNumber { get; set; } = string.Empty;
        public string? Email { get; set; }

        protected Contact() { }

        public Contact(string phoneNumber, string? email = null)
        {
            PhoneNumber = phoneNumber;
            Email = email;
        }
        public void Update(string phoneNumber, string? email = null)
        {
            PhoneNumber = phoneNumber;
            Email = email;
        }
    }
}
