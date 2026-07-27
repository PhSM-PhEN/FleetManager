using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using FleetManager.Application.UseCase.ToVehiclePricing.GetByVehicleId;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.ToVehiclePricing.GetByVehicleId
{
    public class GetByVehicleIdVehiclePricingUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var pricing = VehiclePricingBuilder.Build();

            var useCase = CreateUseCase(pricing);
            var result = await useCase.Execute(pricing.VehicleId);

            result.ShouldNotBeNull();
            result.VehicleId.ShouldBe(pricing.VehicleId);
            result.DailyPrice.ShouldBe(pricing.DailyPrice);
            result.MileagePerMonthly.ShouldBe(pricing.MileagePerMonthly);
        }

        [Fact]
        public async Task Error_Pricing_Not_Found()
        {
            var useCase = CreateUseCase(pricing: null, vehicleId: 999);
            var act = async () => await useCase.Execute(999);

            var result = await act.ShouldThrowAsync<NotFoundException>();
            result.Message.ShouldBe(ResourceErrorMessages.VEHICLE_PRICING_NOT_FOUND);
        }

        private static GetByVehicleIdVehiclePricingUseCase CreateUseCase(FleetManager.Domain.Entities.VehiclePricing? pricing, long? vehicleId = null)
        {
            var repository = new VehiclePricingReadOnlyRepositoryBuilder()
                .GetByVehicleId(vehicleId ?? pricing!.VehicleId, pricing)
                .Build();

            return new GetByVehicleIdVehiclePricingUseCase(repository);
        }
    }
}
