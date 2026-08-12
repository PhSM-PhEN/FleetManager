using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.ToCharge;
using CommonTestUtilities.Repositories.ToContract;
using CommonTestUtilities.Repositories.ToVehicle;
using CommonTestUtilities.Request.ToContract;
using FleetManager.Application.UseCase.ToContract.FinishUp;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Enum;
using FleetManager.Domain.Repositories.ToCharge;
using FleetManager.Domain.Repositories.ToVehicle;
using FleetManager.Exception.ExceptionBase;
using Moq;
using Shouldly;

namespace UseCase.Tests.ToContract.FinishUp
{
    public class FinishUpContractUseCaseTest
    {
        // 5.4 (Incident) — Garantia estrutural: um incidente aberto NUNCA pode impedir o
        // FinishUp, porque essa use case nem tem acesso a repositório de IncidentReport.
        [Fact]
        public void Constructor_Does_Not_Depend_On_Any_IncidentReport_Repository()
        {
            var constructor = typeof(FinishUpContractUseCase).GetConstructors().Single();

            var dependsOnIncidentReport = constructor.GetParameters()
                .Any(p => p.ParameterType.Name.Contains("IncidentReport"));

            dependsOnIncidentReport.ShouldBeFalse();
        }

        [Fact]
        public async Task Success()
        {
            var contract = ContractBuilder.Build(1, vehicleId: 10, status: ContractStatus.Active);
            var finalMileage = contract.StartMileage + contract.MileageContracted;
            var request = RequestFinishUpContractJsonBuilder.Build(finalMileage);

            var useCase = CreateUseCase(contract, out _, out var vehicleRepositoryMock);

            var response = await useCase.Execute(contract.Id, request);

            contract.ContractStatus.ShouldBe(ContractStatus.Finished);
            contract.FinalMileage.ShouldBe(finalMileage);
            response.ContractId.ShouldBe(contract.Id);
            response.FinalMileage.ShouldBe(finalMileage);

            // FinishUp mexe direto na entidade Vehicle: atualiza a quilometragem atual
            // e persiste via IVehicleWriteOnlyRepository (não existe mais use case dedicado pra isso).
            vehicleRepositoryMock.Verify(v => v.Update(
                It.Is<Vehicle>(veh => veh.CurrentMileage == finalMileage)), Times.Once);
        }

        [Fact]
        public async Task Success_Charges_Excess_Mileage_Fee()
        {
            var contract = ContractBuilder.Build(1, vehicleId: 10, status: ContractStatus.Active);
            var finalMileage = contract.StartMileage + contract.MileageContracted + 100;
            var request = RequestFinishUpContractJsonBuilder.Build(finalMileage);

            var useCase = CreateUseCase(contract, out _, out _);

            var response = await useCase.Execute(contract.Id, request);

            var excessMileage = finalMileage - contract.StartMileage - contract.MileageContracted;
            var expectedFee = excessMileage * contract.SnapshotPricePerExtraMileage;
            contract.ExcessMileageFee.ShouldBe(expectedFee);
            response.ExcessMileageFee.ShouldBe(expectedFee);
            response.TotalCharged.ShouldBe(expectedFee);
        }

        [Fact]
        public async Task Success_Charges_Late_Fee_When_Returned_After_Due_Date()
        {
            // devolução prevista há 3 dias e meio -> conta como 4 dias de atraso (fração conta como dia cheio)
            var returnDueDateTime = DateTime.UtcNow.AddDays(-3).AddHours(-13);
            var pickupDateTime = returnDueDateTime.AddDays(-10);
            var contract = ContractBuilder.Build(1, vehicleId: 10, status: ContractStatus.Active,
                rentalType: RentalType.Daily, pickupDateTime: pickupDateTime, returnDueDateTime: returnDueDateTime);
            var finalMileage = contract.StartMileage + contract.MileageContracted;
            var request = RequestFinishUpContractJsonBuilder.Build(finalMileage);

            var useCase = CreateUseCase(contract, out var chargeRepositoryMock, out _);

            var response = await useCase.Execute(contract.Id, request);

            contract.DaysLate.ShouldBe(4);
            contract.LateFee.ShouldBe(4 * contract.SnapshotPriceDailyRate);
            response.DaysLate.ShouldBe(4);
            response.LateFee.ShouldBe(contract.LateFee);
            response.TotalCharged.ShouldBe(contract.LateFee!.Value);
            chargeRepositoryMock.Verify(c => c.Add(It.Is<Charge>(charge =>
                charge.ContractId == contract.Id &&
                charge.Amount == contract.LateFee)), Times.Once);
        }

        [Fact]
        public async Task Success_Charges_Late_Fee_And_Excess_Mileage_Fee_Together()
        {
            // devolução prevista há 3 dias e meio -> 4 dias de atraso, e também acima da km contratada.
            var returnDueDateTime = DateTime.UtcNow.AddDays(-3).AddHours(-13);
            var pickupDateTime = returnDueDateTime.AddDays(-10);
            var contract = ContractBuilder.Build(1, vehicleId: 10, status: ContractStatus.Active,
                rentalType: RentalType.Daily, pickupDateTime: pickupDateTime, returnDueDateTime: returnDueDateTime);
            var finalMileage = contract.StartMileage + contract.MileageContracted + 100;
            var request = RequestFinishUpContractJsonBuilder.Build(finalMileage);

            var useCase = CreateUseCase(contract, out var chargeRepositoryMock, out _);

            var response = await useCase.Execute(contract.Id, request);

            contract.DaysLate.ShouldBe(4);
            contract.LateFee!.Value.ShouldBeGreaterThan(0);
            contract.ExcessMileageFee!.Value.ShouldBeGreaterThan(0);
            response.TotalCharged.ShouldBe(contract.LateFee!.Value + contract.ExcessMileageFee!.Value);

            chargeRepositoryMock.Verify(c => c.Add(It.Is<Charge>(charge =>
                charge.ContractId == contract.Id && charge.Amount == contract.LateFee)), Times.Once);
            chargeRepositoryMock.Verify(c => c.Add(It.Is<Charge>(charge =>
                charge.ContractId == contract.Id && charge.Amount == contract.ExcessMileageFee)), Times.Once);
            chargeRepositoryMock.Verify(c => c.Add(It.IsAny<Charge>()), Times.Exactly(2));
        }

        [Fact]
        public async Task Success_Does_Not_Charge_Late_Fee_When_Returned_On_Time()
        {
            var contract = ContractBuilder.Build(1, vehicleId: 10, status: ContractStatus.Active);
            var finalMileage = contract.StartMileage + contract.MileageContracted;
            var request = RequestFinishUpContractJsonBuilder.Build(finalMileage);
            

            var useCase = CreateUseCase(contract, out var chargeRepositoryMock, out _);

            var response = await useCase.Execute(contract.Id, request);

            contract.DaysLate.ShouldBe(0);
            contract.LateFee.ShouldBe(0);
            response.TotalCharged.ShouldBe(0);
            chargeRepositoryMock.Verify(c => c.Add(It.IsAny<Charge>()), Times.Never);
        }

        [Fact]
        public async Task Error_Contract_Not_Found()
        {
            var useCase = CreateUseCase(contract: null, out _, out _);
            var request = RequestFinishUpContractJsonBuilder.Build(1000);

            var act = async () => await useCase.Execute(999, request);

            var result = await act.ShouldThrowAsync<NotFoundException>();
            result.Message.ShouldBe(ResourceErrorMessages.CONTRACT_NOT_FOUND);
        }

        [Fact]
        public async Task Error_Contract_Not_Active()
        {
            var contract = ContractBuilder.Build(1, vehicleId: 10, status: ContractStatus.Reserved);
            var request = RequestFinishUpContractJsonBuilder.Build(contract.StartMileage + contract.MileageContracted);

            var useCase = CreateUseCase(contract, out _, out _);
            var act = async () => await useCase.Execute(contract.Id, request);

            var result = await act.ShouldThrowAsync<BusinessRuleException>();
            result.Message.ShouldBe(ResourceErrorMessages.CONTRACT_NOT_ACTIVE);
        }

        [Fact]
        public async Task Error_FinalMileage_Less_Than_Start()
        {
            var contract = ContractBuilder.Build(1, vehicleId: 10, status: ContractStatus.Active);
            var request = RequestFinishUpContractJsonBuilder.Build(contract.StartMileage - 1);

            var useCase = CreateUseCase(contract, out _, out _);
            var act = async () => await useCase.Execute(contract.Id, request);

            var result = await act.ShouldThrowAsync<BusinessRuleException>();
            result.Message.ShouldBe(ResourceErrorMessages.END_MILEAGE_CANNOT_BE_LESS_THAN_START);
        }

        [Fact]
        public async Task Error_FinalMileage_Negative()
        {
            var contract = ContractBuilder.Build(1, vehicleId: 10, status: ContractStatus.Active);
            var request = RequestFinishUpContractJsonBuilder.Build(-1);

            var useCase = CreateUseCase(contract, out _, out _);
            var act = async () => await useCase.Execute(contract.Id, request);

            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();
            result.GetErrors().ShouldContain(ResourceErrorMessages.MILEAGE_INVALID);
        }

        private static FinishUpContractUseCase CreateUseCase(
            Contract? contract,
            out Mock<IChargeWriteOnlyRepository> chargeRepositoryMock,
            out Mock<IVehicleWriteOnlyRepository> vehicleRepositoryMock)
        {
            var contractRepositoryBuilder = new ContractWriteOnlyRepositoryBuilder();
            var chargeRepositoryBuilder = new ChargeWriteOnlyRepositoryBuilder().Add();
            var vehicleRepositoryBuilder = new VehicleWriteOnlyRepositoryBuilder();

            if (contract is not null)
            {
                contractRepositoryBuilder.GetById(contract.Id, contract);
                contractRepositoryBuilder.Update(contract);

                var vehicle = VehicleBuilder.Build(id: contract.VehicleId, currentMileage: contract.StartMileage);
                vehicleRepositoryBuilder.GetById(contract.VehicleId, vehicle);
                vehicleRepositoryBuilder.Update(vehicle);
            }
            else
            {
                contractRepositoryBuilder.GetById(999, null);
            }

            var contractRepository = contractRepositoryBuilder.Build();
            chargeRepositoryMock = chargeRepositoryBuilder.BuildMock();

            var vehicleRepository = vehicleRepositoryBuilder.Build();
            vehicleRepositoryMock = Mock.Get(vehicleRepository);

            return new FinishUpContractUseCase(
                contractRepository, chargeRepositoryMock.Object, vehicleRepository, UnitOfWorkBuilder.Build());
        }
    }
}
