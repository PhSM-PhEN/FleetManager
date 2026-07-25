using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using FleetManager.Application.UseCase.ToVehicle.Update;
using FleetManager.Communication.Request.ToVehicle;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.ToVehicle.Update
{
    public class UpdateMileageVehicleUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var vehicle = VehicleBuilder.Build();
            var newMileage = vehicle.CurrentMileage + 100;
            var request = new RequestMileageVehicleJson { MileageVehicle = newMileage };

            var useCase = CreateUseCase(vehicle);
            await useCase.Execute(vehicle.Id, request);

            vehicle.CurrentMileage.ShouldBe(newMileage);
        }

        [Fact]
        public async Task Error_Vehicle_Not_Found()
        {
            var request = new RequestMileageVehicleJson { MileageVehicle = 1000 };

            var useCase = CreateUseCase(vehicle: null);
            var act = async () => await useCase.Execute(999, request);

            var result = await act.ShouldThrowAsync<NotFoundException>();
            result.Message.ShouldBe(ResourceErrorMessages.VEHICLE_NOT_FOUND);
        }

        [Fact]
        public async Task Error_Mileage_Cannot_Decrease()
        {
            var vehicle = VehicleBuilder.Build();
            vehicle.UpdateMileage(vehicle.CurrentMileage + 500); // garante uma base > 0
            var request = new RequestMileageVehicleJson { MileageVehicle = vehicle.CurrentMileage - 1 };

            var useCase = CreateUseCase(vehicle);
            var act = async () => await useCase.Execute(vehicle.Id, request);

            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();
            result.GetErrors().ShouldContain(ResourceErrorMessages.MILEAGE_CANNOT_DECREASE);
        }

        [Fact]
        public async Task Error_Mileage_Invalid_Negative()
        {
            // Este teste cobre a regra do CurrentMiliageValidator (MileageVehicle >= 0).
            // Se falhar, é sinal de que o Validate(request) não está sendo chamado
            // dentro do Execute do UpdateMileageVehicleUseCase.
            var vehicle = VehicleBuilder.Build(companyId: 1);
            var request = new RequestMileageVehicleJson { MileageVehicle = -1 };

            var useCase = CreateUseCase(vehicle);
            var act = async () => await useCase.Execute(vehicle.Id, request);

            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();
            result.GetErrors().ShouldContain(ResourceErrorMessages.MILEAGE_INVALID);
        }

        private static UpdateMileageVehicleUseCase CreateUseCase(FleetManager.Domain.Entities.Vehicle? vehicle)
        {
            var repository = new VehicleWriteOnlyRepositoryBuilder()
                .GetById(vehicle?.Id ?? 999, vehicle!)
                .Build();

            var unitOfWork = UnitOfWorkBuilder.Build();

            return new UpdateMileageVehicleUseCase(repository, unitOfWork);
        }
    }
}
