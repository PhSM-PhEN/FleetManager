namespace FleetManager.Domain.Entities
{
    public class Address : AuditableEntity
    {
        public string Street {get; private set;} = string.Empty;
        public string Number {get; private set;} = string.Empty;
        public string City {get; private set;} = string.Empty;
        public string Neighborhood { get; private set;} = string.Empty; 
        public string State {get; private set;} = string.Empty;
        public string ZipCode {get; private set;} = string.Empty;

        protected Address(){}

        public Address(string street, string number, string city, string neighborhood, string state, string zipCode)
        {
            Street = street;
            Number = number;
            Neighborhood = neighborhood;
            City = city;
            State = state;
            ZipCode = zipCode;
        }
        public void Update(string street, string number, string city, string neighborhood, string state, string zipCode)
        {
            Street = street;
            Number = number;
            City = city;
            Neighborhood = neighborhood;
            State = state;
            ZipCode = zipCode;
            
        }

    }
}
