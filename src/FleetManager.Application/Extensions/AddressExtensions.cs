using FleetManager.Communication.Response.ToAddress;
using FleetManager.Domain.Entities;

namespace FleetManager.Application.Extensions
{
    public static class AddressExtensions
    {

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
        public static List<ResponseAddressJson> ToResponse(List<Address> addresses)
        {
            return addresses.Select(a => a.ToResponse()).ToList();
        }
    }
}
