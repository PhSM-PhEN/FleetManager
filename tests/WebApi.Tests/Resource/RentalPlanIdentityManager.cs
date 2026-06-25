using FleetManager.Domain.Entities;

namespace WebApi.Tests.Resource
{
    public class RentalPlanIdentityManager(RentalPlan rentalPlan)
    {


        public long GetById() => rentalPlan.Id;
    }
}
