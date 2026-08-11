using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.ToCharge;
using CommonTestUtilities.Repositories.ToContract;
using CommonTestUtilities.Request.ToContract;
using FleetManager.Application.UseCase.ToContract.FinishUp;
using FleetManager.Application.UseCase.ToVehicle.Update;
using FleetManager.Communication.Request.ToVehicle;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Enum;
using FleetManager.Exception.ExceptionBase;
using Moq;
using Shouldly;

namespace UseCase.Tests.ToContract.FinishUp
{
    public class CompleteContractUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var contract = ContractBuilder.Build(1, vehicleId: 10, status: ContractStatus.Active);
            var finalMileage = contract.StartMileage + contract.MileageContracted;
            var request = RequestFinishUpContractJsonBuilder.Build(finalMileage);

            var useCase = CreateUseCase(contract, out _, out var updateMileageMock);

            var response = await useCase.Execute(contract.Id, request);

            contract.ContractStatus.ShouldBe(ContractStatus.Finished);
            contract.FinalMileage.ShouldBe(finalMileage);
            response.ContractId.ShouldBe(contract.Id);
            response.FinalMileage.ShouldBe(finalMileage);

            // Complete não mexe no Vehicle diretamente: apenas aciona o caso de uso de
            // quilometragem, passando a km final informada na devolução.
            updateMileageMock.Verify(u => u.Execute(contract.VehicleId,
                It.Is<RequestMileageVehicleJson>(r => r.MileageVehicle == finalMileage)), Times.Once);
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
            var contract = ContractBuilder.Build(1, vehicleId: 10, status: ContractStatus.Active);
            var finalMileage = contract.StartMileage + contract.MileageContracted;
            var request = RequestFinishUpContractJsonBuilder.Build(finalMileage);
            // devolução 3 dias e meio depois do previsto -> conta como 4 dias de atraso (fração conta como dia cheio)
            request.ActualReturnDateTime = contract.ReturnDueDateTime.AddDays(3).AddHours(12);

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
        public async Task Success_Does_Not_Charge_Late_Fee_When_Returned_On_Time()
        {
            var contract = ContractBuilder.Build(1, vehicleId: 10, status: ContractStatus.Active);
            var finalMileage = contract.StartMileage + contract.MileageContracted;
            var request = RequestFinishUpContractJsonBuilder.Build(finalMileage);
            request.ActualReturnDateTime = contract.ReturnDueDateTime;

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
            out Mock<FleetManager.Domain.Repositories.ToCharge.IChargeWriteOnlyRepository> chargeRepositoryMock,
            out Mock<IUpdateMileageVehicleUseCase> updateMileageMock)
        {
            var contractRepositoryBuilder = new ContractWriteOnlyRepositoryBuilder();
            var chargeRepositoryBuilder = new ChargeWriteOnlyRepositoryBuilder().Add();

            if (contract is not null)
            {
                contractRepositoryBuilder.GetById(contract.Id, contract);
                contractRepositoryBuilder.Update(contract);
            }
            else
            {
                contractRepositoryBuilder.GetById(999, null);
            }

            var contractRepository = contractRepositoryBuilder.Build();
            chargeRepositoryMock = chargeRepositoryBuilder.BuildMock();

            // CompleteContractUseCase não fala mais com o repositório de Vehicle: ele delega pro
            // caso de uso de atualização de km, que aqui é apenas mockado (sem lógica própria).
            updateMileageMock = new Mock<IUpdateMileageVehicleUseCase>();
            updateMileageMock
                .Setup(u => u.Execute(It.IsAny<long>(), It.IsAny<RequestMileageVehicleJson>()))
                .Returns(Task.CompletedTask);

            var unitOfWork = UnitOfWorkBuilder.Build();

            return new CompleteContractUseCase(contractRepository, chargeRepositoryMock.Object, updateMileageMock.Object, unitOfWork);
        }
    }
}
