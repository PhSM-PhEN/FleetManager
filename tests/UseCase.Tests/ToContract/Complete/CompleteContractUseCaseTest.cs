using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.ToCharge;
using CommonTestUtilities.Repositories.ToContract;
using CommonTestUtilities.Repositories.ToVehicle;
using CommonTestUtilities.Request.ToContract;
using FleetManager.Application.UseCase.ToContract.Complete;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Enum;
using FleetManager.Exception.ExceptionBase;
using Moq;
using Shouldly;

namespace UseCase.Tests.ToContract.Complete
{
    public class CompleteContractUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var contract = ContractBuilder.Build(1, vehicleId: 10, status: ContractStatus.Active);
            var vehicle = VehicleBuilder.Build(10);

            var finalMileage = Math.Max(contract.StartMileage + contract.MileageContracted, vehicle.CurrentMileage);
            var request = RequestCompleteContractJsonBuilder.Build(finalMileage);

            var useCase = CreateUseCase(contract, vehicle, out _);

            await useCase.Execute(contract.Id, request);

            contract.ContractStatus.ShouldBe(ContractStatus.Finished);
            contract.FinalMileage.ShouldBe(finalMileage);
            vehicle.CurrentMileage.ShouldBe(finalMileage);
        }

        [Fact]
        public async Task Success_Charges_Excess_Mileage_Fee()
        {
            var contract = ContractBuilder.Build(1, vehicleId: 10, status: ContractStatus.Active);
            var vehicle = VehicleBuilder.Build(10);

            // Garante determinismo: independentemente da km aleatória do veículo, o excedente
            // sobre o contratado é sempre recalculado com a mesma fórmula usada no domínio.
            var baseline = Math.Max(contract.StartMileage + contract.MileageContracted, vehicle.CurrentMileage);
            var finalMileage = baseline + 100;
            var request = RequestCompleteContractJsonBuilder.Build(finalMileage);

            var useCase = CreateUseCase(contract, vehicle, out _);

            await useCase.Execute(contract.Id, request);

            var excessMileage = finalMileage - contract.StartMileage - contract.MileageContracted;
            var expectedFee = excessMileage * contract.SnapshotPricePerExtraMileage;
            contract.ExcessMileageFee.ShouldBe(expectedFee);
        }

        [Fact]
        public async Task Success_Charges_Late_Fee_When_Returned_After_Due_Date()
        {
            var contract = ContractBuilder.Build(1, vehicleId: 10, status: ContractStatus.Active);
            var vehicle = VehicleBuilder.Build(10);

            var finalMileage = Math.Max(contract.StartMileage + contract.MileageContracted, vehicle.CurrentMileage);
            var request = RequestCompleteContractJsonBuilder.Build(finalMileage);
            // devolução 3 dias e meio depois do previsto -> conta como 4 dias de atraso (fração conta como dia cheio)
            request.ActualReturnDateTime = contract.ReturnDueDateTime.AddDays(3).AddHours(12);

            var useCase = CreateUseCase(contract, vehicle, out var chargeRepositoryMock);

            await useCase.Execute(contract.Id, request);

            contract.DaysLate.ShouldBe(4);
            contract.LateFee.ShouldBe(4 * contract.SnapshotPriceDailyRate);
            chargeRepositoryMock.Verify(c => c.Add(It.Is<Charge>(charge =>
                charge.ContractId == contract.Id &&
                charge.Amount == contract.LateFee)), Times.Once);
        }

        [Fact]
        public async Task Success_Does_Not_Charge_Late_Fee_When_Returned_On_Time()
        {
            var contract = ContractBuilder.Build(1, vehicleId: 10, status: ContractStatus.Active);
            var vehicle = VehicleBuilder.Build(10);

            var finalMileage = Math.Max(contract.StartMileage + contract.MileageContracted, vehicle.CurrentMileage);
            var request = RequestCompleteContractJsonBuilder.Build(finalMileage);
            request.ActualReturnDateTime = contract.ReturnDueDateTime;

            var useCase = CreateUseCase(contract, vehicle, out var chargeRepositoryMock);

            await useCase.Execute(contract.Id, request);

            contract.DaysLate.ShouldBe(0);
            contract.LateFee.ShouldBe(0);
            chargeRepositoryMock.Verify(c => c.Add(It.IsAny<Charge>()), Times.Never);
        }

        [Fact]
        public async Task Error_Contract_Not_Found()
        {
            var useCase = CreateUseCase(contract: null, vehicle: null, out _);
            var request = RequestCompleteContractJsonBuilder.Build(1000);

            var act = async () => await useCase.Execute(999, request);

            var result = await act.ShouldThrowAsync<NotFoundException>();
            result.Message.ShouldBe(ResourceErrorMessages.CONTRACT_NOT_FOUND);
        }

        [Fact]
        public async Task Error_Contract_Not_Active()
        {
            var contract = ContractBuilder.Build(1, vehicleId: 10, status: ContractStatus.Reserved);
            var vehicle = VehicleBuilder.Build(10);
            var request = RequestCompleteContractJsonBuilder.Build(contract.StartMileage + contract.MileageContracted);

            var useCase = CreateUseCase(contract, vehicle, out _);
            var act = async () => await useCase.Execute(contract.Id, request);

            var result = await act.ShouldThrowAsync<BusinessRuleException>();
            result.Message.ShouldBe(ResourceErrorMessages.CONTRACT_NOT_ACTIVE);
        }

        [Fact]
        public async Task Error_FinalMileage_Less_Than_Start()
        {
            var contract = ContractBuilder.Build(1, vehicleId: 10, status: ContractStatus.Active);
            var vehicle = VehicleBuilder.Build(10);
            var request = RequestCompleteContractJsonBuilder.Build(contract.StartMileage - 1);

            var useCase = CreateUseCase(contract, vehicle, out _);
            var act = async () => await useCase.Execute(contract.Id, request);

            var result = await act.ShouldThrowAsync<BusinessRuleException>();
            result.Message.ShouldBe(ResourceErrorMessages.END_MILEAGE_CANNOT_BE_LESS_THAN_START);
        }

        [Fact]
        public async Task Error_FinalMileage_Negative()
        {
            var contract = ContractBuilder.Build(1, vehicleId: 10, status: ContractStatus.Active);
            var vehicle = VehicleBuilder.Build(10);
            var request = RequestCompleteContractJsonBuilder.Build(-1);

            var useCase = CreateUseCase(contract, vehicle, out _);
            var act = async () => await useCase.Execute(contract.Id, request);

            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();
            result.GetErrors().ShouldContain(ResourceErrorMessages.MILEAGE_INVALID);
        }

        private static CompleteContractUseCase CreateUseCase(Contract? contract, Vehicle? vehicle, out Mock<FleetManager.Domain.Repositories.ToCharge.IChargeWriteOnlyRepository> chargeRepositoryMock)
        {
            var contractRepositoryBuilder = new ContractWriteOnlyRepositoryBuilder();
            var vehicleRepositoryBuilder = new VehicleWriteOnlyRepositoryBuilder();
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

            if (vehicle is not null)
            {
                vehicleRepositoryBuilder.GetById(vehicle.Id, vehicle);
                vehicleRepositoryBuilder.Update(vehicle);
            }

            var contractRepository = contractRepositoryBuilder.Build();
            var vehicleRepository = vehicleRepositoryBuilder.Build();
            chargeRepositoryMock = chargeRepositoryBuilder.BuildMock();
            var unitOfWork = UnitOfWorkBuilder.Build();

            return new CompleteContractUseCase(contractRepository, chargeRepositoryMock.Object, vehicleRepository, unitOfWork);
        }
    }
}

