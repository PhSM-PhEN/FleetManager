using FleetManager.Domain.Entities;

namespace WebApi.Tests.Resource
{
    public class VehicleIdentitiyManager(Vehicle vehicle)
    {
        private readonly Vehicle _vehicle = vehicle;

        public long GetById() => _vehicle.Id;
    }
}
