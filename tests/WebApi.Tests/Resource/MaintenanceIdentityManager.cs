using FleetManager.Domain.Entities;

namespace WebApi.Tests.Resource
{
    public class MaintenanceIdentityManager(Maintenance maintenance)
    {
        public long GetById() => maintenance.Id;
    }
}
