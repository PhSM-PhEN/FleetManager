using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories.ToContract;
using CommonTestUtilities.Repositories.ToTenant;
using CommonTestUtilities.Repositories.ToVehicle;
using CommonTestUtilities.Request.ToContract;
using FleetManager.Application.UseCase.ToContract.Preview;
using FleetManager.Domain.Entities;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.ToContract.Preview
{
    public class PreviewContractUseCaseTest
    {
        [Fact]
        public async Task Success_Daily()
        {
            var vehicle = VehicleBuilder.Build(1);
            vehicle.RentalPlan = RentalPlanBuilder.Build(1);
            var tenant = TenantBuilder.Build(1);
            var request = RequestPreviewContractJsonBuilder.Build(vehicle.Id, tenant.Id, "Daily");

            var useCase = CreateUseCase(vehicle, tenant, hasActiveContract: false);
            var result = await useCase.Execute(request);

            result.ShouldNotBeNull();
            result.VehicleId.ShouldBe(vehicle.Id);
            result.TenantId.ShouldBe(tenant.Id);
            result.RentalPlanId.ShouldBe(vehicle.RentalPlan.Id);
            result.TotalDays.ShouldBeGreaterThan(0);
            result.TotalAmount.ShouldBeGreaterThan(0);
            result.MileageContracted.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task Success_Monthly()
        {
            var vehicle = VehicleBuilder.Build(1);
            vehicle.RentalPlan = RentalPlanBuilder.Build(1);
            var tenant = TenantBuilder.Build(1);
            var request = RequestPreviewContractJsonBuilder.Build(vehicle.Id, tenant.Id, "Monthly");

            var useCase = CreateUseCase(vehicle, tenant, hasActiveContract: false);
            var result = await useCase.Execute(request);

            result.TotalDays.ShouldBe(30);
            result.TotalAmount.ShouldBe(vehicle.RentalPlan.MonthlyPrice);
            result.MileageContracted.ShouldBe(vehicle.RentalPlan.MileagePerMonthly);
        }

        [Fact]
        public async Task Error_Vehicle_Not_Found()
        {
            var tenant = TenantBuilder.Build(1);
            var request = RequestPreviewContractJsonBuilder.Build(999, tenant.Id);

            var useCase = CreateUseCase(vehicle: null, tenant, hasActiveContract: false);
            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<NotFoundException>();
            result.Message.ShouldBe(ResourceErrorMessages.VEHICLE_NOT_FOUND);
        }

        [Fact]
        public async Task Error_Vehicle_Not_Active()
        {
            var vehicle = VehicleBuilder.Build(1);
            vehicle.RentalPlan = RentalPlanBuilder.Build(1);
            vehicle.Desactivate();
            var tenant = TenantBuilder.Build(1);
            var request = RequestPreviewContractJsonBuilder.Build(vehicle.Id, tenant.Id);

            var useCase = CreateUseCase(vehicle, tenant, hasActiveContract: false);
            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<BusinessRuleException>();
            result.Message.ShouldBe(ResourceErrorMessages.VEHICLE_NOT_AVAILABLE);
        }

        [Fact]
        public async Task Error_Vehicle_Already_Rented()
        {
            var vehicle = VehicleBuilder.Build(1);
            vehicle.RentalPlan = RentalPlanBuilder.Build(1);
            var tenant = TenantBuilder.Build(1);
            var request = RequestPreviewContractJsonBuilder.Build(vehicle.Id, tenant.Id);

            var useCase = CreateUseCase(vehicle, tenant, hasActiveContract: true);
            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<BusinessRuleException>();
            result.Message.ShouldBe(ResourceErrorMessages.VEHICLE_ALREADY_RENTED);
        }

        [Fact]
        public async Task Error_Tenant_Not_Found()
        {
            var vehicle = VehicleBuilder.Build(1);
            vehicle.RentalPlan = RentalPlanBuilder.Build(1);
            var request = RequestPreviewContractJsonBuilder.Build(vehicle.Id, 999);

            var useCase = CreateUseCase(vehicle, tenant: null, hasActiveContract: false);
            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<NotFoundException>();
            result.Message.ShouldBe(ResourceErrorMessages.TENANT_NOT_FOUND);
        }

        [Fact]
        public async Task Error_Tenant_Not_Active()
        {
            var vehicle = VehicleBuilder.Build(1);
            vehicle.RentalPlan = RentalPlanBuilder.Build(1);
            var tenant = TenantBuilder.Build(1);
            tenant.Disable();
            var request = RequestPreviewContractJsonBuilder.Build(vehicle.Id, tenant.Id);

            var useCase = CreateUseCase(vehicle, tenant, hasActiveContract: false);
            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<BusinessRuleException>();
            result.Message.ShouldBe(ResourceErrorMessages.TENANT_NOT_AVAILABLE);
        }

        [Fact]
        public async Task Error_RentalType_Invalid()
        {
            var vehicle = VehicleBuilder.Build(1);
            vehicle.RentalPlan = RentalPlanBuilder.Build(1);
            var tenant = TenantBuilder.Build(1);
            var request = RequestPreviewContractJsonBuilder.Build(vehicle.Id, tenant.Id);
            request.RentalType = "Weekly";

            var useCase = CreateUseCase(vehicle, tenant, hasActiveContract: false);
            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();
            result.GetErrors().ShouldContain(ResourceErrorMessages.RENTAL_TYPE_INVALID);
        }

        private static PreviewContractUseCase CreateUseCase(Vehicle? vehicle, Tenant? tenant, bool hasActiveContract)
        {
            var vehicleRepositoryBuilder = new VehicleReadOnlyRepositoryBuilder();
            if (vehicle is not null)
                vehicleRepositoryBuilder.GetById(vehicle.Id, vehicle);

            var tenantRepositoryBuilder = new TenantReadOnlyRepositoryBuilder();
            if (tenant is not null)
                tenantRepositoryBuilder.GetById(tenant, tenant.Id);

            var contractRepository = new ContractWriteOnlyRepositoryBuilder()
                .HasActiveContract(vehicle?.Id ?? 0, hasActiveContract)
                .Build();

            var vehicleRepository = vehicleRepositoryBuilder.Build();
            var tenantRepository = tenantRepositoryBuilder.Build();

            return new PreviewContractUseCase(vehicleRepository, tenantRepository, contractRepository);
        }
    }
}
