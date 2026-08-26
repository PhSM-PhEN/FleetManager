using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.ToContract;
using CommonTestUtilities.Repositories.ToIncidentReport;
using CommonTestUtilities.Repositories.ToVehicle;
using FleetManager.Application.UseCase.ToIncidentReport.Register;
using FleetManager.Communication.Request.ToIncidentReport;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Enum;
using FleetManager.Domain.Repositories.ToContract;
using FleetManager.Domain.Repositories.ToVehicle;
using FleetManager.Exception.ExceptionBase;
using Moq;
using Shouldly;

namespace UseCase.Tests.ToIncidentReport.Register
{
    public class RegisterIncidentReportUseCaseTest
    {
        [Fact]
        public async Task Error_Vehicle_Not_Found()
        {
            var contract = ContractBuilder.Build(1);
            var request = BuildRequest(contract.Id, vehicleId: 999, "High");

            var useCase = CreateUseCase(vehicle: null, contract: contract);
            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<NotFoundException>();
            result.Message.ShouldBe(ResourceErrorMessages.VEHICLE_NOT_FOUND);
        }

        [Fact]
        public async Task Error_Contract_Not_Found()
        {
            var vehicle = VehicleBuilder.Build(id: 10);
            var request = BuildRequest(contractId: 999, vehicle.Id, "High");

            var useCase = CreateUseCase(vehicle: vehicle, contract: null);
            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<NotFoundException>();
            result.Message.ShouldBe(ResourceErrorMessages.CONTRACT_NOT_FOUND);
        }

        [Fact]
        public async Task Error_Invalid_Risk()
        {
            var vehicle = VehicleBuilder.Build(id: 10);
            var contract = ContractBuilder.Build(1);
            var request = BuildRequest(contract.Id, vehicle.Id, "Medium");

            var useCase = CreateUseCase(vehicle, contract);
            var act = async () => await useCase.Execute(request);

            await act.ShouldThrowAsync<ErrorOnValidationException>();
        }

        // 5.1 — Bloqueio: Risk = High -> bloqueia o veículo.
        [Fact]
        public async Task Success_High_Risk_Blocks_Vehicle()
        {
            var vehicle = VehicleBuilder.Build(id: 10);
            var contract = ContractBuilder.Build(1);
            var request = BuildRequest(contract.Id, vehicle.Id, "High");

            var useCase = CreateUseCase(vehicle, contract, out var vehicleRepositoryMock);

            var response = await useCase.Execute(request);
 
            response.IncidentRisk.ShouldBe("High");
            vehicleRepositoryMock.Verify(v => v.Update(vehicle), Times.Once);
        }

        // 5.1 — Bloqueio: Risk != High -> NÃO bloqueia o veículo.
        [Fact]
        public async Task Success_Low_Risk_Does_Not_Block_Vehicle()
        {
            var vehicle = VehicleBuilder.Build(id: 10);
            var contract = ContractBuilder.Build(1);
            var request = BuildRequest(contract.Id, vehicle.Id, "Low");

            var useCase = CreateUseCase(vehicle, contract, out var vehicleRepositoryMock);

            var response = await useCase.Execute(request);

 
            response.IncidentRisk.ShouldBe("Low");
            vehicleRepositoryMock.Verify(v => v.Update(It.IsAny<Vehicle>()), Times.Never);
        }

        [Fact]
        public async Task Error_High_Risk_On_Already_Blocked_Vehicle()
        {
            var vehicle = VehicleBuilder.Build(id: 10);
            var blockingIncident = IncidentReportBuilder.Build(vehicleId: vehicle.Id, incidentRisk: IncidentRisk.High);
            vehicle.BlockForIncident(blockingIncident);
            var contract = ContractBuilder.Build(1);
            var request = BuildRequest(contract.Id, vehicle.Id, "High");

            var useCase = CreateUseCase(vehicle, contract);
            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<BusinessRuleException>();
            result.Message.ShouldBe(ResourceErrorMessages.VEHICLE_ALREADY_BLOCKED_FOR_MAINTENANCE);
        }

        private static RequestIncidentReportJson BuildRequest(long contractId, long vehicleId, string incidentRisk)
        {
            return new RequestIncidentReportJson
            {
                ContractId = contractId,
                //VehicleId = vehicleId,
                Description = "Risco identificado durante a vistoria",
                IncidentRisk = incidentRisk
            };
        }

        private static RegisterIncidentReportUseCase CreateUseCase(Vehicle? vehicle, Contract? contract)
        {
            return CreateUseCase(vehicle, contract, out _);
        }

        private static RegisterIncidentReportUseCase CreateUseCase(Vehicle? vehicle, Contract? contract,
            out Mock<IVehicleWriteOnlyRepository> vehicleRepositoryMock)
        {
            var vehicleRepositoryBuilder = new VehicleWriteOnlyRepositoryBuilder();
            if (vehicle is not null)
            {
                vehicleRepositoryBuilder.GetById(vehicle.Id, vehicle);
                vehicleRepositoryBuilder.Update(vehicle);
            }
            else
            {
                vehicleRepositoryBuilder.GetById(999, null!);
            }

            var contractRepositoryBuilder = new ContractReadOnlyRepositoryBuilder();
            if (contract is not null)
                contractRepositoryBuilder.GetById(contract.Id, contract);
            else
                contractRepositoryBuilder.GetById(999, null);

            var incidentReportRepository = new IncidentReportWriteOnlyRepositoryBuilder().Add().Build();
            var vehicleRepository = vehicleRepositoryBuilder.Build();
            vehicleRepositoryMock = Mock.Get(vehicleRepository);
            var contractRepository = contractRepositoryBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();

            return new RegisterIncidentReportUseCase(incidentReportRepository, unitOfWork, vehicleRepository, contractRepository);
        }
    }
}
