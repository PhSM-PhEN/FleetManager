using FleetManager.Communication.Responses;
using FleetManager.Domain.Entities;

namespace FleetManager.Application.Extensions
{
    public static class ClientExtensions
    {
        public static ResponseShortClientJson ToShortResponse(this Client client)
        {
            return new ResponseShortClientJson
            {
                Id = client.Id,
                FirstAndLastName = client.FirstAndLastName,
                PhoneNumber = client.PhoneNumber
            };
        }
        public static ResponseClientJson ToDetailResponse(this Client client)
        {
            return new ResponseClientJson
            {
                Id = client.Id,
                FirstAndLastName = client.FirstAndLastName,
                PhoneNumber = client.PhoneNumber,
                RG = client.RG,
                CPF = client.CPF,
                CnhRegisterNumber = client.CnhRegisterNumber,
                CnhCategory = client.CnhCategory,
                AddressId = client.AddressId,
                Address = client.Address.ToResponse()
                
            };
        }
        public static List<ResponseShortClientJson> ToShortResponse(this List<Client> clients)
        {
            return clients.Select(c => c.ToShortResponse()).ToList();
        }

        
    }
}
