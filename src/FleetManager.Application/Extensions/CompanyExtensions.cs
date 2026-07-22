using FleetManager.Communication.Response.ToCompany;
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
                Address = company.Address.ToResponse()
            };
        }

        public static ResponseShortCompanyJson ToShortResponse(this Company company)
        {
            return new ResponseShortCompanyJson
            {
                Id = company.Id,
                Name = company.Name,
                Cnpj = company.Cnpj
            };
        }

        public static List<ResponseShortCompanyJson> ToShortResponse(this List<Company> companies)
        {
            return companies.Select(c => c.ToShortResponse()).ToList();
        }
    }
}