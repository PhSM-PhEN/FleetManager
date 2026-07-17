using FleetManager.Communication.Response.ToAddress;
using FleetManager.Domain.Entities;

namespace FleetManager.Application.Extensions
{
    public static class AddressExtensions
    {
        public static ResponseShortAddressJson ToShortResponse(this Address address)
        {
            return new ResponseShortAddressJson
            {
                Id = address.Id,
                Street = address.Street,
                ZipCode = address.ZipCode
            };
        }
        public static ResponseAddressJson ToResponse(this Address address)
        {
            return new ResponseAddressJson
            {
                Id = address.Id,
                Street = address.Street,
                Number = address.Number,
                City = address.City,
                State = address.State,
                ZipCode = address.ZipCode,
            };
        }
        public static List<ResponseShortAddressJson> ToShortResponse(this List<Address> addresses)
        {
            return addresses.Select(a => a.ToShortResponse()).ToList();
        }
    }
}
