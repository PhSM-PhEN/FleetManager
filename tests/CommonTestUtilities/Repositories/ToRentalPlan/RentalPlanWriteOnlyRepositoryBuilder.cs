using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToRentalPlan;
using Moq;

namespace CommonTestUtilities.Repositories.ToRentalPlan
{
    public class RentalPlanWriteOnlyRepositoryBuilder
    {
        private readonly Mock<IRentalPlanWriteOnlyRepository> _repository;

        public RentalPlanWriteOnlyRepositoryBuilder()
        {
            _repository = new Mock<IRentalPlanWriteOnlyRepository>();
        }

        public RentalPlanWriteOnlyRepositoryBuilder Add(RentalPlan rentalPlan)
        {
            _repository.Setup(p => p.Add(rentalPlan)).Returns(Task.CompletedTask);
            return this;
        }

        public RentalPlanWriteOnlyRepositoryBuilder GetById(RentalPlan rentalPlan)
        {
            _repository.Setup(p => p.GetById(rentalPlan.Id)).ReturnsAsync(rentalPlan);
            return this;
        }

        public RentalPlanWriteOnlyRepositoryBuilder Update(RentalPlan rentalPlan)
        {
            _repository.Setup(p => p.Update(rentalPlan));
            return this;
        }

        public IRentalPlanWriteOnlyRepository Build() => _repository.Object;
    }
}
