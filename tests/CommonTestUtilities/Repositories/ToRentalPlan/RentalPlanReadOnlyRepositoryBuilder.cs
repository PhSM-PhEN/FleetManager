using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToRentalPlan;
using Moq;

namespace CommonTestUtilities.Repositories.ToRentalPlan
{
    public class RentalPlanReadOnlyRepositoryBuilder
    {
        private readonly Mock<IRentalPlanReadOnlyRepository> _repository;

        public RentalPlanReadOnlyRepositoryBuilder()
        {
            _repository = new Mock<IRentalPlanReadOnlyRepository>();
        }

        public RentalPlanReadOnlyRepositoryBuilder GetById(RentalPlan rentalPlan)
        {
            _repository.Setup(p => p.GetById(rentalPlan.Id)).ReturnsAsync(rentalPlan);
            return this;
        }

        public IRentalPlanReadOnlyRepository Build() => _repository.Object;
    }
}
