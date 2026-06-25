using FleetManager.Communication.Responses;
using FleetManager.Domain.Entities;

namespace FleetManager.Application.Extensions
{
    public static class CompanyExtensions
    {
        public static ResponseCompanyJson ToResponse(this Company company)
        {
            return new ResponseCompanyJson
            {
                Id = company.Id,
                Name = company.Name,
                Cnpj = company.Cnpj,
                PhoneNumber = company.PhoneNumber,
                AddressId = company.AddressId,
                Address = company.Address.ToResponse()
            };
            
        }
        public static List<ResponseCompanyJson> Toresponse(this List<Company> companies)
        {
            return companies.Select(c => c.ToResponse()).ToList();
        }
    }
}
