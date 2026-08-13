using CommonTestUtilities.Entities;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Enum;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace Domain.Tests.Entities
{
    /// <summary>
    /// Cobre os pontos do checklist de Renew que ainda faltavam: estados que não estão na
    /// matriz principal (ContractStateMachineTest), preservação do snapshot histórico,
    /// comportamento com/sem novo plano, e o mileage inicial do contrato renovado.
    /// </summary>
    public class ContractRenewTest
    {
        [Fact]
        public void Reserved_Renew_Is_Not_Allowed()
        {
            var contract = ContractBuilder.Build(1, status: ContractStatus.Reserved);
            var rentalPlan = RentalPlanBuilder.Build(id: contract.RentalPlanId);

            var exception = Should.Throw<BusinessRuleException>(
                () => Contract.Renew(contract, rentalPlan, null));

            exception.Message.ShouldBe(ResourceErrorMessages.CONTRACT_NOT_ACTIVE);
        }

        [Fact]
        public void Renewed_Renew_Is_Not_Allowed()
        {
            var contract = ContractBuilder.Build(1, status: ContractStatus.Renewed);
            var rentalPlan = RentalPlanBuilder.Build(id: contract.RentalPlanId);

            var exception = Should.Throw<BusinessRuleException>(
                () => Contract.Renew(contract, rentalPlan, null));

            exception.Message.ShouldBe(ResourceErrorMessages.CONTRACT_NOT_ACTIVE);
        }

        [Fact]
        public void Renew_Without_Plan_Change_Keeps_Same_RentalPlanId()
        {
            var rentalPlan = RentalPlanBuilder.Build(id: 1);
            var contract = ContractBuilder.Build(1, status: ContractStatus.Active, rentalPlan: rentalPlan);

            var renewedContract = Contract.Renew(contract, rentalPlan, null);

            renewedContract.RentalPlanId.ShouldBe(contract.RentalPlanId);
        }

        [Fact]
        public void Renew_With_New_Plan_Uses_New_Plan_Price()
        {
            var originalPlan = RentalPlanBuilder.Build(id: 1);
            var contract = ContractBuilder.Build(1, status: ContractStatus.Active,
                rentalType: RentalType.Daily, rentalPlan: originalPlan);
            var newPlan = RentalPlanBuilder.Build(id: 2);

            var renewedContract = Contract.Renew(contract, newPlan, null);

            renewedContract.RentalPlanId.ShouldBe(newPlan.Id);
            renewedContract.TotalAmount.ShouldBe(newPlan.DailyPrice * contract.TotalDays);
            renewedContract.SnapshotPriceDailyRate.ShouldBe(newPlan.DailyPrice);
            renewedContract.SnapshotPricePerExtraMileage.ShouldBe(newPlan.ExcessMileageRate);
        }

        [Fact]
        public void Renew_Preserves_Previous_Contract_Historical_Snapshot_After_Plan_Price_Change()
        {
            var rentalPlan = RentalPlanBuilder.Build(id: 1);
            var contract = ContractBuilder.Build(1, status: ContractStatus.Active, rentalPlan: rentalPlan);
            var originalSnapshotPrice = contract.SnapshotPriceDailyRate;

            // Reajuste de preço no plano DEPOIS que o contrato original já existia.
            rentalPlan.Update(originalSnapshotPrice * 2, rentalPlan.MonthlyPrice * 2,
                rentalPlan.ExcessMileageRate, rentalPlan.MileagePerDay, rentalPlan.MileagePerMonthly);

            Contract.Renew(contract, rentalPlan, null);

            // O contrato ORIGINAL não pode ser afetado pelo reajuste: seu snapshot é histórico.
            contract.SnapshotPriceDailyRate.ShouldBe(originalSnapshotPrice);
        }

        [Fact]
        public void Renew_TotalAmount_Uses_Frozen_Price_When_Plan_Unchanged_Even_If_Price_Later_Increases()
        {
            var rentalPlan = RentalPlanBuilder.Build(id: 1);
            var contract = ContractBuilder.Build(1, status: ContractStatus.Active,
                rentalType: RentalType.Daily, rentalPlan: rentalPlan);
            var originalSnapshotPrice = contract.SnapshotPriceDailyRate;
            var originalSnapshotExtraMileageRate = contract.SnapshotPricePerExtraMileage;

            rentalPlan.Update(originalSnapshotPrice * 2, rentalPlan.MonthlyPrice * 2,
                rentalPlan.ExcessMileageRate * 2, rentalPlan.MileagePerDay, rentalPlan.MileagePerMonthly);

            var renewedContract = Contract.Renew(contract, rentalPlan, null);

            // Mesmo plano (não trocado) -> deve cobrar pelo preço que valia no contrato original,
            // não pelo preço reajustado depois. E o snapshot do contrato NOVO precisa refletir
            // o mesmo preço congelado, não o preço atual do plano.
            renewedContract.TotalAmount.ShouldBe(originalSnapshotPrice * contract.TotalDays);
            renewedContract.SnapshotPriceDailyRate.ShouldBe(originalSnapshotPrice);
            renewedContract.SnapshotPricePerExtraMileage.ShouldBe(originalSnapshotExtraMileageRate);
        }

        [Fact]
        public void Renew_New_Contract_Starts_At_Previous_Contract_Expected_End_Mileage()
        {
            var rentalPlan = RentalPlanBuilder.Build(id: 1);
            var contract = ContractBuilder.Build(1, status: ContractStatus.Active, rentalPlan: rentalPlan);

            var renewedContract = Contract.Renew(contract, rentalPlan, null);

            // O novo contrato nunca pode começar do zero (StartMileage) do contrato anterior:
            // tem que continuar de onde o contrato anterior estava previsto para terminar.
            renewedContract.StartMileage.ShouldBe(contract.ExpectedEndMileage);
            renewedContract.StartMileage.ShouldNotBe(contract.StartMileage);
        }

        [Fact]
        public void Renew_Without_MileageContractedOverride_Keeps_Previous_MileageContracted()
        {
            var rentalPlan = RentalPlanBuilder.Build(id: 1);
            var contract = ContractBuilder.Build(1, status: ContractStatus.Active, rentalPlan: rentalPlan);

            var renewedContract = Contract.Renew(contract, rentalPlan, null);

            renewedContract.MileageContracted.ShouldBe(contract.MileageContracted);
        }

        [Fact]
        public void Renew_With_MileageContractedOverride_Uses_The_Override()
        {
            var rentalPlan = RentalPlanBuilder.Build(id: 1);
            var contract = ContractBuilder.Build(1, status: ContractStatus.Active, rentalPlan: rentalPlan);
            var overrideMileage = contract.MileageContracted + 500;

            var renewedContract = Contract.Renew(contract, rentalPlan, overrideMileage);

            renewedContract.MileageContracted.ShouldBe(overrideMileage);
        }
    }
}
