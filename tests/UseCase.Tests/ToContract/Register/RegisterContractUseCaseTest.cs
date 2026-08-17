using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.ToContract;
using CommonTestUtilities.Repositories.ToRentalPlan;
using CommonTestUtilities.Repositories.ToTenant;
using CommonTestUtilities.Repositories.ToVehicle;
using CommonTestUtilities.Request.ToContract;
using FleetManager.Application.UseCase.ToContract.Register;
using FleetManager.Domain.Entities;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.ToContract.Register
{
    public class RegisterContractUseCaseTest
    {
        [Fact]
        public async Task Success_Daily()
        {
            var vehicle = VehicleBuilder.Build(1);
            var tenant = TenantBuilder.Build(1);
            var rentalPlan = RentalPlanBuilder.Build(1);
            var request = RequestContractJsonBuilder.Build(vehicle.Id, tenant.Id, rentalPlan.Id, "Daily");

            var useCase = CreateUseCase(vehicle, tenant, rentalPlan, hasActiveContract: false);
            var result = await useCase.Execute(request);

            result.ShouldNotBeNull();
            result.ContractStatus.ShouldNotBeNullOrEmpty();
        }

        [Fact]
        public async Task Success_Monthly()
        {
            var vehicle = VehicleBuilder.Build(1);
            var tenant = TenantBuilder.Build(1);
            var rentalPlan = RentalPlanBuilder.Build(1);
            var request = RequestContractJsonBuilder.Build(vehicle.Id, tenant.Id, rentalPlan.Id, "Monthly");

            var useCase = CreateUseCase(vehicle, tenant, rentalPlan, hasActiveContract: false);
            var result = await useCase.Execute(request);

            result.ShouldNotBeNull();
            result.TotalDays.ShouldBe(30);
        }

        [Fact]
        public async Task Error_Vehicle_Not_Found()
        {
            var tenant = TenantBuilder.Build(1);
            var rentalPlan = RentalPlanBuilder.Build(1);
            var request = RequestContractJsonBuilder.Build(999, tenant.Id, rentalPlan.Id);

            var useCase = CreateUseCase(vehicle: null, tenant, rentalPlan, hasActiveContract: false);
            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<NotFoundException>();
            result.Message.ShouldBe(ResourceErrorMessages.VEHICLE_NOT_FOUND);
        }

        [Fact]
        public async Task Error_Tenant_Not_Found()
        {
            var vehicle = VehicleBuilder.Build(1);
            var rentalPlan = RentalPlanBuilder.Build(1);
            var request = RequestContractJsonBuilder.Build(vehicle.Id, 999, rentalPlan.Id);

            var useCase = CreateUseCase(vehicle, tenant: null, rentalPlan, hasActiveContract: false);
            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<NotFoundException>();
            result.Message.ShouldBe(ResourceErrorMessages.TENANT_NOT_FOUND);
        }

        [Fact]
        public async Task Error_RentalPlan_Not_Found()
        {
            var vehicle = VehicleBuilder.Build(1);
            var tenant = TenantBuilder.Build(1);
            var request = RequestContractJsonBuilder.Build(vehicle.Id, tenant.Id, 999);

            var useCase = CreateUseCase(vehicle, tenant, rentalPlan: null, hasActiveContract: false);
            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<NotFoundException>();
            result.Message.ShouldBe(ResourceErrorMessages.RENTAL_PLAN_NOT_FOUND);
        }

        [Fact]
        public async Task Error_Vehicle_Already_Rented()
        {
            var vehicle = VehicleBuilder.Build(1);
            var tenant = TenantBuilder.Build(1);
            var rentalPlan = RentalPlanBuilder.Build(1);
            var request = RequestContractJsonBuilder.Build(vehicle.Id, tenant.Id, rentalPlan.Id);

            var useCase = CreateUseCase(vehicle, tenant, rentalPlan, hasActiveContract: true);
            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<BusinessRuleException>();
            result.Message.ShouldBe(ResourceErrorMessages.VEHICLE_ALREADY_RENTED);
        }

        [Fact]
        public async Task Error_VehicleId_Not_Greater_Than_Zero()
        {
            var vehicle = VehicleBuilder.Build(1);
            var tenant = TenantBuilder.Build(1);
            var rentalPlan = RentalPlanBuilder.Build(1);
            var request = RequestContractJsonBuilder.Build(vehicle.Id, tenant.Id, rentalPlan.Id);
            request.VehicleId = 0;

            var useCase = CreateUseCase(vehicle, tenant, rentalPlan, hasActiveContract: false);
            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();
            result.GetErrors().ShouldContain(ResourceErrorMessages.VEHICLE_ID_REQUIRED);
        }
        [Fact]
        public async Task Error_Vehicle_Blocked_For_Maintenance()
        {
            var vehicle = VehicleBuilder.Build(1);
            var incidentReport = IncidentReportBuilder.Build(vehicleId: vehicle.Id);
            vehicle.BlockForIncident(incidentReport); 

            var tenant = TenantBuilder.Build(1);
            var rentalPlan = RentalPlanBuilder.Build(1);
            var request = RequestContractJsonBuilder.Build(vehicle.Id, tenant.Id, rentalPlan.Id);

            var useCase = CreateUseCase(vehicle, tenant, rentalPlan, hasActiveContract: false);
            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<BusinessRuleException>();
            result.Message.ShouldBe(ResourceErrorMessages.VEHICLE_BLOCKED_FOR_MAINTENANCE);
        }

        [Fact]
        public async Task Error_Vehicle_Not_Active()
        {
            var vehicle = VehicleBuilder.Build(1);
            vehicle.Deactivate();

            var tenant = TenantBuilder.Build(1);
            var rentalPlan = RentalPlanBuilder.Build(1);
            var request = RequestContractJsonBuilder.Build(vehicle.Id, tenant.Id, rentalPlan.Id);

            var useCase = CreateUseCase(vehicle, tenant, rentalPlan, hasActiveContract: false);
            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<BusinessRuleException>();
            result.Message.ShouldBe(ResourceErrorMessages.VEHICLE_NOT_AVAILABLE);
        }

        [Fact]
        public async Task Error_Tenant_Not_Active()
        {
            var vehicle = VehicleBuilder.Build(1);
            var tenant = TenantBuilder.Build(1);
            tenant.Deactivate();

            var rentalPlan = RentalPlanBuilder.Build(1);
            var request = RequestContractJsonBuilder.Build(vehicle.Id, tenant.Id, rentalPlan.Id);

            var useCase = CreateUseCase(vehicle, tenant, rentalPlan, hasActiveContract: false);
            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<BusinessRuleException>();
            result.Message.ShouldBe(ResourceErrorMessages.TENANT_NOT_AVAILABLE);
        }

        private static RegisterContractUseCase CreateUseCase(Vehicle? vehicle, Tenant? tenant, RentalPlan? rentalPlan, bool hasActiveContract)
        {
            var vehicleRepositoryBuilder = new VehicleReadOnlyRepositoryBuilder();
            if (vehicle is not null)
                vehicleRepositoryBuilder.GetById(vehicle.Id, vehicle);

            var tenantRepositoryBuilder = new TenantReadOnlyRepositoryBuilder();
            if (tenant is not null)
                tenantRepositoryBuilder.GetById(tenant, tenant.Id);

            var rentalPlanRepositoryBuilder = new RentalPlanReadOnlyRepositoryBuilder();
            if (rentalPlan is not null)
                rentalPlanRepositoryBuilder.GetById(rentalPlan);

            var contractRepository = new ContractWriteOnlyRepositoryBuilder()
                .HasActiveContract(vehicle?.Id ?? 0, hasActiveContract)
                .Build();

            var vehicleRepository = vehicleRepositoryBuilder.Build();
            var tenantRepository = tenantRepositoryBuilder.Build();
            var rentalPlanRepository = rentalPlanRepositoryBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();

            return new RegisterContractUseCase(vehicleRepository, tenantRepository, rentalPlanRepository, contractRepository, unitOfWork);
        }
    }
}