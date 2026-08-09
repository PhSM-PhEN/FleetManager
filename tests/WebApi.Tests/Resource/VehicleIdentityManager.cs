using FleetManager.Domain.Entities;

namespace WebApi.Tests.Resource
{
    public class VehicleIdentityManager(Vehicle vehicle)
    {
        public long GetById() => vehicle.Id;
        public long GetCurrentMileage() => vehicle.CurrentMileage;
    }
}
