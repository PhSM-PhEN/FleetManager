using FleetManager.Communication.Response.ToRenant;
using FleetManager.Domain.Entities;

namespace FleetManager.Application.Extensions;

public static class TenantExtensions
{
    public static ResponseTenantJson ToResponse(this Tenant tenant)
    {
        return  new ResponseTenantJson
        {
            Id = tenant.Id,
            Name = tenant.Name,
            PhoneNumber = tenant.Contact.PhoneNumber
        };
        
    }
    public static ResponseInfoTenantJson ToInfoResponse(this Tenant tenant)
    {
        return new ResponseInfoTenantJson
        {
            Id = tenant.Id,
            Name = tenant.Name,
            Cpf = tenant.Cpf.Number,
            RG = tenant.RG,
            DriverLicenseNumber = tenant.DriverLicense.Number,
            DriverLicenseCategory = tenant.DriverLicense.Category,
            PhoneNumber = tenant.Contact.PhoneNumber,
            Email = tenant.Contact.Email,
            Address = tenant.Address.ToResponse() 
        };
    }
    public static List<ResponseTenantJson> ToResponse(this List<Tenant> tenants)
    {
        return tenants.Select(t => t.ToResponse()).ToList();
    }
}
