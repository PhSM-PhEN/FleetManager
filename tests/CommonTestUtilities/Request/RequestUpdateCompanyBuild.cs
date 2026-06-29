using Bogus;
using FleetManager.Communication.Requests;

namespace CommonTestUtilities.Request
{
    public class RequestUpdateCompanyBuild
    {
        public static RequestUpdateCompanyJson Build(long addressId = 1)
        {
            return new Faker<RequestUpdateCompanyJson>()
                    .RuleFor(r => r.Name, f => f.Person.FullName)
                    .RuleFor(r => r.PhoneNumber, f => f.Phone.PhoneNumber("(##) #####-####")) 
                    .RuleFor(r => r.AddressId, _ = addressId)
                    .Generate();
        }
    }
}