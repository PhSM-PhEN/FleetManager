using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Request.ToVehiclePricing;
using FleetManager.Application.UseCase.ToVehiclePricing.Update;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.ToVehiclePricing.Update
{
    public class UpdateVehiclePricingUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var pricing = VehiclePricingBuilder.Build();
            var request = RequestVehiclePricingJsonBuilder.Build(pricing.VehicleId);

            var useCase = CreateUseCase(pricing);
            await useCase.Execute(pricing.VehicleId, request);

            pricing.DailyPrice.ShouldBe(request.DailyPrice);
            pricing.MonthlyPrice.ShouldBe(request.MonthlyPrice);
            pricing.ExcessMileageRate.ShouldBe(request.ExcessMileageRate);
            pricing.MileagePerDay.ShouldBe(request.MileagePerDay);
            pricing.MileagePerMonthly.ShouldBe(request.MileagePerMonthly);
        }

        [Fact]
        public async Task Error_Pricing_Not_Found()
        {
            var request = RequestVehiclePricingJsonBuilder.Build(999);

            var useCase = CreateUseCase(pricing: null, vehicleId: 999);
            var act = async () => await useCase.Execute(999, request);

            var result = await act.ShouldThrowAsync<NotFoundException>();
            result.Message.ShouldBe(ResourceErrorMessages.VEHICLE_PRICING_NOT_FOUND);
        }

        [Fact]
        public async Task Error_MonthlyPrice_Zero()
        {
            var pricing = VehiclePricingBuilder.Build();
            var request = RequestVehiclePricingJsonBuilder.Build(pricing.VehicleId);
            request.MonthlyPrice = 0;

            var useCase = CreateUseCase(pricing);
            var act = async () => await useCase.Execute(pricing.VehicleId, request);

            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();
            result.GetErrors().ShouldContain(ResourceErrorMessages.MONTHLY_PRICE_INVALID);
        }

        private static UpdateVehiclePricingUseCase CreateUseCase(FleetManager.Domain.Entities.VehiclePricing? pricing, long? vehicleId = null)
        {
            var repository = new VehiclePricingWriteOnlyRepositoryBuilder()
                .GetByVehicleId(vehicleId ?? pricing!.VehicleId, pricing)
                .Build();

            var unitOfWork = UnitOfWorkBuilder.Build();

            return new UpdateVehiclePricingUseCase(repository, unitOfWork);
        }
    }
}
