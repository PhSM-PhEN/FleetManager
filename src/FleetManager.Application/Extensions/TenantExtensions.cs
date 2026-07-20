using FleetManager.Communication.Response.ToRenant;
using FleetManager.Domain.Entities;

namespace FleetManager.Application.Extensions;

public static class TenantExtensions
{
    public static ResponseRegiserTenantJson ToResponseRegister(this Tenant tenant)
    {
        return  new ResponseRegiserTenantJson
        {
            Id = tenant.Id,
            Name = tenant.Name,
            PhoneNumber = tenant.Contact.PhoneNumber
        };
        
    }
}
