using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Request.ToVehiclePricing;
using FleetManager.Application.UseCase.ToVehiclePricing.Update;
using FleetManager.Domain.Entities;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.ToVehiclePricing.Update
{
    public class UpdateVehiclePricingUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var pricing = VehiclePricingBuilder.Build(1);
            var request = RequestVehiclePricingJsonBuilder.Build();

            var useCase = CreateUseCase(pricing);
            await useCase.Execute(pricing.Id, request);

            pricing.DailyPrice.ShouldBe(request.DailyPrice);
            pricing.MonthlyPrice.ShouldBe(request.MonthlyPrice);
            pricing.ExcessMileageRate.ShouldBe(request.ExcessMileageRate);
            pricing.MileagePerDay.ShouldBe(request.MileagePerDay);
            pricing.MileagePerMonthly.ShouldBe(request.MileagePerMonthly);
        }

        [Fact]
        public async Task Error_Pricing_Not_Found()
        {
            var pricing = VehiclePricingBuilder.Build(1);
            var request = RequestVehiclePricingJsonBuilder.Build();

            var useCase = CreateUseCase(pricing);
            var act = async () => await useCase.Execute(999, request);

            var result = await act.ShouldThrowAsync<NotFoundException>();
            result.Message.ShouldBe(ResourceErrorMessages.VEHICLE_PRICING_NOT_FOUND);
        }

        [Fact]
        public async Task Error_MonthlyPrice_Zero()
        {
            var pricing = VehiclePricingBuilder.Build();
            var request = RequestVehiclePricingJsonBuilder.Build();
            request.MonthlyPrice = 0;

            var useCase = CreateUseCase(pricing);
            var act = async () => await useCase.Execute(pricing.Id, request);

            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();
            result.GetErrors().ShouldContain(ResourceErrorMessages.MONTHLY_PRICE_INVALID);
        }

        private static UpdateVehiclePricingUseCase CreateUseCase(VehiclePricing pricing)
        {
            var repository = new VehiclePricingWriteOnlyRepositoryBuilder()
                .GetById(pricing)
                .Build();

            var unitOfWork = UnitOfWorkBuilder.Build();

            return new UpdateVehiclePricingUseCase(repository, unitOfWork);
        }
    }
}
