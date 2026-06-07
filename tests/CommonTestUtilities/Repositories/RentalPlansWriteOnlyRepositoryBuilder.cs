using FleetManager.Domain.Repositories.ToRentalPlans;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class RentalPlansWriteOnlyRepositoryBuilder
    {
        public static IRentalPlansWriteOnlyRepository Build()
        {
            var mock = new Mock<IRentalPlansWriteOnlyRepository>();
            
            return mock.Object;
        }
    }
}
