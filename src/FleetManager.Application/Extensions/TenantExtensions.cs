using FleetManager.Communication.Response;
using FleetManager.Communication.Response.ToTenant;
using FleetManager.Domain.Entities;

namespace FleetManager.Application.Extensions;

public static class TenantExtensions
{
    public static ResponseShortTenantJson ToResponse(this Tenant tenant)
    {
        return  new ResponseShortTenantJson
        {
            Id = tenant.Id,
            Name = tenant.Name,
            PhoneNumber = tenant.Contact.PhoneNumber,
            Status = new ResponseEnumStatusJson
            {
                Id = (int)tenant.Status,
                Label = tenant.Status.ToString()
            },
        };
        
    }
    public static ResponseTenantJson ToInfoResponse(this Tenant tenant)
    {
        return new ResponseTenantJson
        {
            Id = tenant.Id,
            Name = tenant.Name,
            Cpf = tenant.Cpf.Number,
            RG = tenant.RG,
            DriverLicenseNumber = tenant.DriverLicense.Number,
            DriverLicenseCategory = tenant.DriverLicense.Category,
            PhoneNumber = tenant.Contact.PhoneNumber,
            Email = tenant.Contact.Email,
            Status = new ResponseEnumStatusJson
            {
                Id = (int)tenant.Status,
                Label = tenant.Status.ToString()
            },
            Address = tenant.Address.ToResponse()
        };
    }
    public static List<ResponseShortTenantJson> ToResponse(this List<Tenant> tenants)
    {
        return [.. tenants.Select(t => t.ToResponse())];
    }
}
