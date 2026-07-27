using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Request.ToVehiclePricing;
using FleetManager.Application.UseCase.ToVehiclePricing.Register;
using FleetManager.Domain.Entities;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.ToVehiclePricing.Register
{
    public class RegisterVehiclePricingUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var vehiclePricing = VehiclePricingBuilder.Build(1);
            var request = RequestVehiclePricingJsonBuilder.Build();

            var useCase = CreateUseCase(vehiclePricing);
            var result = await useCase.Execute(request);

            result.ShouldNotBeNull();
            result.Name.ShouldBe(request.Name);
            result.DailyPrice.ShouldBe(request.DailyPrice);
        }

        [Fact]
        public async Task Error_DailyPrice_Zero()
        {
            var vehiclePricing = VehiclePricingBuilder.Build(1);
            var request = RequestVehiclePricingJsonBuilder.Build();
            request.DailyPrice = 0;

            var useCase = CreateUseCase(vehiclePricing);
            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();
            result.GetErrors().ShouldContain(ResourceErrorMessages.DAILY_PRICE_INVALID);
        }


        private static RegisterVehiclePricingUseCase CreateUseCase(VehiclePricing vehiclePricing)
        {
            var writeRepository = new VehiclePricingWriteOnlyRepositoryBuilder()
                .Add(vehiclePricing)
                .Build();
            var unitOfWork = UnitOfWorkBuilder.Build();

            return new RegisterVehiclePricingUseCase(writeRepository, unitOfWork);
        }
    }
}
