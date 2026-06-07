using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToRentalPlans;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class RentalPlansReadOnlyRepositoryBuilder
    {
        private readonly Mock<IRentalPlansReadOnlyRepository> _repository;

        public RentalPlansReadOnlyRepositoryBuilder()
        {
            _repository = new Mock<IRentalPlansReadOnlyRepository>();
        }
        public RentalPlansReadOnlyRepositoryBuilder GetAll(List<RentalPlan> rentalPlans)
        {
            _repository.Setup(r => r.GetAll()).ReturnsAsync(rentalPlans);
            return this;
        }

        public IRentalPlansReadOnlyRepository Build() => _repository.Object;
    }
}
