using FleetManager.Domain.Entities;

namespace WebApi.Tests.Resource
{
    
    public class RentalIdentityManager(Rental rental)
    {

        public long GetById() => rental.Id;    
        public DateTime GetStartDate() => rental.StartDate;
        public DateTime GetEndDate() => rental.EndDate;
    }
}
