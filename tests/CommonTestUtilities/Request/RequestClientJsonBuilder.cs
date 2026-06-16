using Bogus;
using FleetManager.Communication.Requests;

namespace CommonTestUtilities.Request
{
    public class RequestClientJsonBuilder
    {
        public static RequestClientJson Build(long addressId)
        {
            return new Faker<RequestClientJson>()
                .RuleFor(r => r.FirstAndLastName,   f => f.Name.FullName())
                .RuleFor(r => r.PhoneNumber,        f => f.Phone.PhoneNumber())
                .RuleFor(r => r.RG,                 f => f.Random.Replace("##.###.###-#"))
                .RuleFor(r => r.CPF,                f => f.Random.Replace("###.###.###-##"))
                .RuleFor(r => r.CnhRegisterNumber,  f => f.Random.Replace("#########"))
                .RuleFor(r => r.CnhCategory,        f => f.PickRandom("A", "B", "C", "D", "E", "AB"))
                .RuleFor(r => r.AddressId,          _ => addressId)
                .Generate();
        }
    }
    
}


