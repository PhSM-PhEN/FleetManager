using FleetManager.Communication.Response;
using FleetManager.Communication.Response.ToCompany;
using FleetManager.Domain.Entities;
using FleetManager.Domain.EnumExtensions;

namespace FleetManager.Application.Extensions
{
    public static class CompanyExtensions
    {
        public static ResponseCompanyJson ToResponse(this Company company)
        {
            return new ResponseCompanyJson
            {
                Id = company.Id,
                LegalName = company.LegalName,
                TradeName = company.TradeName,
                Cnpj = company.Cnpj,
                
                PhoneNumber = company.Contact.PhoneNumber,
                Email = company.Contact?.Email,
                Address = company.Address.ToResponse(),
                Status = new ResponseEnumStatusJson
                {
                    Id = (int)company.Status,
                    Label = company.Status.ToStringStatus()
                }
            };
        }
         

        

        public static ResponseShortCompanyJson ToShortResponse(this Company company)
        {
            return new ResponseShortCompanyJson
            {
                Id = company.Id,
           
                Cnpj = company.Cnpj,
                Status = new ResponseEnumStatusJson
                {
                    Id = (int)company.Status,
                    Label = company.Status.ToStringStatus()
                }

            };
        }

        public static List<ResponseShortCompanyJson> ToShortResponse(this List<Company> companies)
        {
            return [.. companies.Select(c => c.ToShortResponse())];
        }
    }
}
