using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.ToVehicle;
using FleetManager.Application.UseCase.ToVehicle.Delete;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.ToVehicle.Delete
{
    public class DeleteVehicleUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var vehicle = VehicleBuilder.Build();
            var useCase = CreateUseCase(vehicle);

            await useCase.Execute(vehicle.Id);
        }

        [Fact]
        public async Task Error_Vehicle_Not_Found()
        {
            var useCase = CreateUseCase(vehicle: null);
            var act = async () => await useCase.Execute(999);

            var result = await act.ShouldThrowAsync<NotFoundException>();
            result.Message.ShouldBe(ResourceErrorMessages.VEHICLE_NOT_FOUND);
        }

        private static DeleteVehicleUseCase CreateUseCase(FleetManager.Domain.Entities.Vehicle? vehicle)
        {
            var repositoryBuilder = new VehicleWriteOnlyRepositoryBuilder()
                .GetById(vehicle?.Id ?? 999, vehicle!);

            if (vehicle is not null)
                repositoryBuilder.Delete(vehicle);

            var repository = repositoryBuilder.Build();

            var unitOfWork = UnitOfWorkBuilder.Build();

            return new DeleteVehicleUseCase(repository, unitOfWork);
        }
    }
}
