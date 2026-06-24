using FleetManager.Domain.Entities;

namespace WebApi.Tests.Resource
{
    public class VehicleIdentitiyManager(Vehicle vehicle)
    {
        public long GetById() => vehicle.Id;
    }
}
