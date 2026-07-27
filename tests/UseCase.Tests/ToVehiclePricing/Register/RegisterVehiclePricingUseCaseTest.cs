using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Request.ToVehiclePricing;
using FleetManager.Application.UseCase.ToVehiclePricing.Register;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.ToVehiclePricing.Register
{
    public class RegisterVehiclePricingUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var vehicle = VehicleBuilder.Build();
            var request = RequestVehiclePricingJsonBuilder.Build(vehicle.Id);

            var useCase = CreateUseCase(vehicle, existingPricing: null);
            var result = await useCase.Execute(request);

            result.ShouldNotBeNull();
            result.VehicleId.ShouldBe(vehicle.Id);
            result.DailyPrice.ShouldBe(request.DailyPrice);
        }

        [Fact]
        public async Task Error_DailyPrice_Zero()
        {
            var vehicle = VehicleBuilder.Build();
            var request = RequestVehiclePricingJsonBuilder.Build(vehicle.Id);
            request.DailyPrice = 0;

            var useCase = CreateUseCase(vehicle, existingPricing: null);
            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();
            result.GetErrors().ShouldContain(ResourceErrorMessages.DAILY_PRICE_INVALID);
        }

        [Fact]
        public async Task Error_Vehicle_Not_Found()
        {
            var request = RequestVehiclePricingJsonBuilder.Build(999);

            var useCase = CreateUseCase(vehicle: null, existingPricing: null);
            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<NotFoundException>();
            result.Message.ShouldBe(ResourceErrorMessages.VEHICLE_NOT_FOUND);
        }

        [Fact]
        public async Task Error_Pricing_Already_Exists()
        {
            var vehicle = VehicleBuilder.Build();
            var existingPricing = VehiclePricingBuilder.Build(vehicleId: vehicle.Id);
            var request = RequestVehiclePricingJsonBuilder.Build(vehicle.Id);

            var useCase = CreateUseCase(vehicle, existingPricing);
            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<BusinessRuleException>();
            result.GetErrors().ShouldContain(ResourceErrorMessages.VEHICLE_PRICING_ALREADY_EXISTS);
        }

        private static RegisterVehiclePricingUseCase CreateUseCase(FleetManager.Domain.Entities.Vehicle? vehicle, FleetManager.Domain.Entities.VehiclePricing? existingPricing)
        {
            var writeRepository = new VehiclePricingWriteOnlyRepositoryBuilder()
                .GetByVehicleId(vehicle?.Id ?? 999, existingPricing)
                .Build();

            var vehicleRepository = new VehicleReadOnlyRepositoryBuilder()
                .GetById(vehicle?.Id ?? 999, vehicle!)
                .Build();

            var unitOfWork = UnitOfWorkBuilder.Build();

            return new RegisterVehiclePricingUseCase(writeRepository, vehicleRepository, unitOfWork);
        }
    }
}
