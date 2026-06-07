using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToRentalPlans;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class RentalPlansUpdateOnlyRepositoryBuilder
    {
        private readonly Mock<IRentalPlansUpdateOnlyRepository> _repository;

        public RentalPlansUpdateOnlyRepositoryBuilder()
        {
            _repository = new Mock<IRentalPlansUpdateOnlyRepository>();
        }
        public RentalPlansUpdateOnlyRepositoryBuilder GetById(RentalPlan rentalPlan)
        {
            _repository.Setup(r => r.GetById(rentalPlan.Id)).ReturnsAsync(rentalPlan);
            return this;
        }

        public IRentalPlansUpdateOnlyRepository Build() => _repository.Object;
    }
}
