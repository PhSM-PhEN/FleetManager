using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToRentalPlan;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class VehiclePricingWriteOnlyRepositoryBuilder
    {
        private readonly Mock<IRentalPlanWriteOnlyRepository> _repository;

        public VehiclePricingWriteOnlyRepositoryBuilder()
        {
            _repository = new Mock<IRentalPlanWriteOnlyRepository>();
        }

        public VehiclePricingWriteOnlyRepositoryBuilder Add(RentalPlan rentalPlan)
        {
            _repository.Setup(p => p.Add(rentalPlan)).Returns(Task.CompletedTask);
            return this;
        }

        public VehiclePricingWriteOnlyRepositoryBuilder GetById(RentalPlan rentalPlan)
        {
            _repository.Setup(p => p.GetById(rentalPlan.Id)).ReturnsAsync(rentalPlan);
            return this;
        }

        public VehiclePricingWriteOnlyRepositoryBuilder Update(RentalPlan rentalPlan)
        {
            _repository.Setup(p => p.Update(rentalPlan));
            return this;
        }

        public IRentalPlanWriteOnlyRepository Build() => _repository.Object;
    }
}
