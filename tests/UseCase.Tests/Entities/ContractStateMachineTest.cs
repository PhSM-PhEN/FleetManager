using CommonTestUtilities.Entities;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Enum;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.Entities
{
    /// <summary>
    /// Matriz oficial de transições de estado do Contract. Cada teste corresponde a uma linha:
    ///
    /// Estado atual | Operação       | Resultado
    /// Reserved     | Activate       | Active
    /// Reserved     | Cancel         | Cancelled
    /// Reserved     | FinishUp       | ❌ (coberto em ContractTest)
    /// Active       | Cancel         | Cancelled
    /// Active       | FinishUp       | Finished (coberto em ContractTest)
    /// Active       | Renew          | novo contrato
    /// Active       | DetectOverdue  | Overdue
    /// Overdue      | FinishUp       | Finished (coberto em ContractTest)
    /// Overdue      | Renew ≤ limite | novo contrato
    /// Overdue      | Renew > limite | ❌
    /// Finished     | Renew          | ❌
    /// Finished     | FinishUp       | ❌ (coberto em ContractTest)
    /// Cancelled    | Activate       | ❌
    /// Cancelled    | Renew          | ❌
    /// </summary>
    public class ContractStateMachineTest
    {
        [Fact]
        public void Reserved_Activate_Results_In_Active()
        {
            var contract = ContractBuilder.Build(1, status: ContractStatus.Reserved);

            contract.Confirm();

            contract.Status.ShouldBe(ContractStatus.Active);
        }

        [Fact]
        public void Reserved_Cancel_Results_In_Cancelled()
        {
            var contract = ContractBuilder.Build(1, status: ContractStatus.Reserved);

            contract.Cancel();

            contract.Status.ShouldBe(ContractStatus.Cancelled);
        }

        [Fact]
        public void Active_Cancel_Results_In_Cancelled()
        {
            var contract = ContractBuilder.Build(1, status: ContractStatus.Active);

            contract.Cancel();

            contract.Status.ShouldBe(ContractStatus.Cancelled);
        }

        [Fact]
        public void Active_Renew_Results_In_New_Contract()
        {
            var contract = ContractBuilder.Build(1, status: ContractStatus.Active);
            var rentalPlan = RentalPlanBuilder.Build(id: contract.RentalPlanId);

            var renewedContract = Contract.Renew(contract, rentalPlan, null);

            contract.Status.ShouldBe(ContractStatus.Renewed);
            renewedContract.Status.ShouldBe(ContractStatus.Active);
            renewedContract.StartMileage.ShouldBe(contract.ExpectedEndMileage);
        }

        [Fact]
        public void Active_DetectOverdue_Results_In_Overdue()
        {
            var contract = ContractBuilder.Build(1, status: ContractStatus.Active);

            contract.MarkAsOverdue();

            contract.Status.ShouldBe(ContractStatus.Overdue);
        }

        // 3.1 — Reforço em nível de domínio: MarkAsOverdue só pode partir de Active. Isso é o
        // que garante, na raiz, que Overdue/Finished/Cancelled/Renewed "permanecem" como estão
        // mesmo que a rotina de detecção seja chamada de forma indevida sobre eles.
        [Theory]
        [InlineData(ContractStatus.Reserved)]
        [InlineData(ContractStatus.Overdue)]
        [InlineData(ContractStatus.Finished)]
        [InlineData(ContractStatus.Cancelled)]
        [InlineData(ContractStatus.Renewed)]
        public void DetectOverdue_Error_When_Status_Is_Not_Active(ContractStatus status)
        {
            var contract = ContractBuilder.Build(1, status: status);

            var exception = Should.Throw<BusinessRuleException>(() => contract.MarkAsOverdue());

            exception.Message.ShouldBe(ResourceErrorMessages.CONTRACT_NOT_ACTIVE);
            contract.Status.ShouldBe(status);
        }

        [Fact]
        public void Overdue_Renew_Within_Limit_Results_In_New_Contract()
        {
            // 2 dias de atraso -> dentro do limite de 3 dias permitido pra renovação.
            var returnDueDateTime = DateTime.UtcNow.AddDays(-2);
            var pickupDateTime = returnDueDateTime.AddDays(-10);
            var contract = ContractBuilder.Build(1, status: ContractStatus.Overdue,
                rentalType: RentalType.Daily, pickupDateTime: pickupDateTime, returnDueDateTime: returnDueDateTime);
            var rentalPlan = RentalPlanBuilder.Build(id: contract.RentalPlanId);

            var renewedContract = Contract.Renew(contract, rentalPlan, null);

            contract.Status.ShouldBe(ContractStatus.Renewed);
            renewedContract.Status.ShouldBe(ContractStatus.Active);
        }

        [Fact]
        public void Overdue_Renew_Past_Limit_Is_Not_Allowed()
        {
            // 4 dias de atraso -> passou do limite de 3 dias permitido pra renovação.
            var returnDueDateTime = DateTime.UtcNow.AddDays(-4);
            var pickupDateTime = returnDueDateTime.AddDays(-10);
            var contract = ContractBuilder.Build(1, status: ContractStatus.Overdue,
                rentalType: RentalType.Daily, pickupDateTime: pickupDateTime, returnDueDateTime: returnDueDateTime);
            var rentalPlan = RentalPlanBuilder.Build(id: contract.RentalPlanId);

            var exception = Should.Throw<BusinessRuleException>(
                () => Contract.Renew(contract, rentalPlan, null));

            exception.Message.ShouldBe(ResourceErrorMessages.RENEWAL_NOT_ALLOWED_PAST_MAX_OVERDUE_DAYS);
            contract.Status.ShouldBe(ContractStatus.Overdue);
        }

        [Fact]
        public void Finished_Renew_Is_Not_Allowed()
        {
            var contract = ContractBuilder.Build(1, status: ContractStatus.Finished);
            var rentalPlan = RentalPlanBuilder.Build(id: contract.RentalPlanId);

            var exception = Should.Throw<BusinessRuleException>(
                () => Contract.Renew(contract, rentalPlan, null));

            exception.Message.ShouldBe(ResourceErrorMessages.CONTRACT_NOT_ACTIVE);
        }

        [Fact]
        public void Cancelled_Activate_Is_Not_Allowed()
        {
            var contract = ContractBuilder.Build(1, status: ContractStatus.Cancelled);

            var exception = Should.Throw<BusinessRuleException>(
                () => contract.Confirm());

            exception.Message.ShouldBe(ResourceErrorMessages.CONTRACT_NOT_RESERVED);
        }

        [Fact]
        public void Cancelled_Renew_Is_Not_Allowed()
        {
            var contract = ContractBuilder.Build(1, status: ContractStatus.Cancelled);
            var rentalPlan = RentalPlanBuilder.Build(id: contract.RentalPlanId);

            var exception = Should.Throw<BusinessRuleException>(
                () => Contract.Renew(contract, rentalPlan, null));

            exception.Message.ShouldBe(ResourceErrorMessages.CONTRACT_NOT_ACTIVE);
        }
    }
}
