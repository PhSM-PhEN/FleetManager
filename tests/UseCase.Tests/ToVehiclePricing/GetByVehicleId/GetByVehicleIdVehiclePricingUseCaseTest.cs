using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using FleetManager.Application.UseCase.ToVehiclePricing.GetByVehicleId;
using FleetManager.Domain.Entities;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.ToVehiclePricing.GetByVehicleId
{
    public class GetByVehicleIdVehiclePricingUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var pricing = VehiclePricingBuilder.Build(1);

            var useCase = CreateUseCase(pricing);
            var result = await useCase.Execute(pricing.Id);

            result.ShouldNotBeNull();
            result.Name.ShouldBe(pricing.Name);
            result.DailyPrice.ShouldBe(pricing.DailyPrice);
            result.MileagePerMonthly.ShouldBe(pricing.MileagePerMonthly);
        }

        [Fact]
        public async Task Error_Pricing_Not_Found()
        {
            var pricing = VehiclePricingBuilder.Build(1);
            var useCase = CreateUseCase(pricing);
            var act = async () => await useCase.Execute(999);

            var result = await act.ShouldThrowAsync<NotFoundException>();
            result.Message.ShouldBe(ResourceErrorMessages.VEHICLE_PRICING_NOT_FOUND);
        }

        private static GetByVehicleIdVehiclePricingUseCase CreateUseCase(VehiclePricing vehiclePricing)
        {
            var repository = new VehiclePricingReadOnlyRepositoryBuilder()
                .GetByVehicleId(vehiclePricing)
                .Build();

            return new GetByVehicleIdVehiclePricingUseCase(repository);
        }
    }
}
