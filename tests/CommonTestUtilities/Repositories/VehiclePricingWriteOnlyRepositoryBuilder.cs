using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToVehiclePricing;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class VehiclePricingWriteOnlyRepositoryBuilder
    {
        private readonly Mock<IVehiclePricingWriteOnlyRepository> _repository;

        public VehiclePricingWriteOnlyRepositoryBuilder()
        {
            _repository = new Mock<IVehiclePricingWriteOnlyRepository>();
        }

        public VehiclePricingWriteOnlyRepositoryBuilder Add(VehiclePricing pricing)
        {
            _repository.Setup(p => p.Add(pricing)).Returns(Task.CompletedTask);
            return this;
        }

        public VehiclePricingWriteOnlyRepositoryBuilder GetByVehicleId(VehiclePricing pricing)
        {
            _repository.Setup(p => p.GetByVehicleId(pricing.Id)).ReturnsAsync(pricing);
            return this;
        }

        public VehiclePricingWriteOnlyRepositoryBuilder Update(VehiclePricing pricing)
        {
            _repository.Setup(p => p.Update(pricing));
            return this;
        }

        public IVehiclePricingWriteOnlyRepository Build() => _repository.Object;
    }
}
