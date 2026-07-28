using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories.ToVehicle;
using FleetManager.Application.UseCase.ToVehicle.GetById;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.ToVehicle.GetById
{
    public class GetByIdVehicleUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var company = CompanyBuilder.Build();
            var vehicle = VehicleBuilder.Build(companyId: company.Id);
            vehicle.Company = company;

            var useCase = CreateUseCase(vehicle);
            var result = await useCase.Execute(vehicle.Id);

            result.ShouldNotBeNull();
            result.Brand.ShouldBe(vehicle.Brand);
            result.LicensePlate.ShouldBe(vehicle.LicensePlate.Number);
            result.Company.Id.ShouldBe(company.Id);
        }

        [Fact]
        public async Task Error_Vehicle_Not_Found()
        {
            var useCase = CreateUseCase(vehicle: null);
            var act = async () => await useCase.Execute(999);

            var result = await act.ShouldThrowAsync<NotFoundException>();
            result.Message.ShouldBe(ResourceErrorMessages.VEHICLE_NOT_FOUND);
        }

        private static GetByIdVehicleUseCase CreateUseCase(FleetManager.Domain.Entities.Vehicle? vehicle)
        {
            var repository = new VehicleReadOnlyRepositoryBuilder()
                .GetById(vehicle?.Id ?? 999, vehicle!)
                .Build();

            return new GetByIdVehicleUseCase(repository);
        }
    }
}
