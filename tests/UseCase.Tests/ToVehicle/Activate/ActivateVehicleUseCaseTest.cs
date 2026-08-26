using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.ToVehicle;
using FleetManager.Application.UseCase.ToVehicle.Activate;
using FleetManager.Domain.Entities;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.ToVehicle.Activate
{
    public class ActivateVehicleUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var vehicle = VehicleBuilder.Build(1);
            vehicle.Deactivate();

            var useCase = CreateUseCase(vehicle);
            var act = async () => await useCase.Execute(vehicle.Id);

            await act.ShouldNotThrowAsync();

        }

        [Fact]
        public async Task Error_Vehicle_Not_Found()
        {
            var useCase = CreateUseCase(vehicle: null);
            var act = async () => await useCase.Execute(999);

            var result = await act.ShouldThrowAsync<NotFoundException>();
            result.Message.ShouldBe(ResourceErrorMessages.VEHICLE_NOT_FOUND);
        }

        [Fact]
        public async Task Error_Already_Active()
        {
            var vehicle = VehicleBuilder.Build(1);

            var useCase = CreateUseCase(vehicle);
            var act = async () => await useCase.Execute(vehicle.Id);

            var result = await act.ShouldThrowAsync<BusinessRuleException>();
            result.Message.ShouldBe(ResourceErrorMessages.VEHICLE_ALREADY_ACTIVE);
        }

        private static ActivateVehicleUseCase CreateUseCase(Vehicle? vehicle)
        {
            var repositoryBuilder = new VehicleWriteOnlyRepositoryBuilder();

            if (vehicle is not null)
                repositoryBuilder.GetById(vehicle.Id, vehicle);

            var repository = repositoryBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();

            return new ActivateVehicleUseCase(repository, unitOfWork);
        }
    }
}
