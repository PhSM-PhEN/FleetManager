using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToVehiclePricing;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class VehiclePricingReadOnlyRepositoryBuilder
    {
        private readonly Mock<IVehiclePricingReadOnlyRepository> _repository;

        public VehiclePricingReadOnlyRepositoryBuilder()
        {
            _repository = new Mock<IVehiclePricingReadOnlyRepository>();
        }

        public VehiclePricingReadOnlyRepositoryBuilder GetById(VehiclePricing pricing)
        {
            _repository.Setup(p => p.GetById(pricing.Id)).ReturnsAsync(pricing);
            return this;
        }

        public IVehiclePricingReadOnlyRepository Build() => _repository.Object;
    }
}
