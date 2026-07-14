namespace FleetManager.Domain.Entities
{
    public class Address : AudiTableEntity
    {
        public string Street {get; private set;} = string.Empty;
        public string Number {get; private set;} = string.Empty;
        public string City {get; private set;} = string.Empty;
        public string State {get; private set;} = string.Empty;
        public string ZipCode {get; private set;} = string.Empty;

        protected Address(){}

        public Address(string street, string number, string city, string state, string zipCode)
        {
            Street = street;
            Number = number;
            City = city;
            State = state;
            ZipCode = zipCode;
        }
        public void Update(string street, string number, string city, string state, string zipCode)
        {
            Street = street;
            Number = number;
            City = city;
            State = state;
            ZipCode = zipCode;
        }

    }
}
