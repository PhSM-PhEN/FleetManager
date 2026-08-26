using CommonTestUtilities.Entities;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Enum;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.Entities
{
    public class ContractTest
    {
        // 1.1 — Estados a partir dos quais o FinishUp pode acontecer.
        [Theory]
        [InlineData(ContractStatus.Active)]
        [InlineData(ContractStatus.Overdue)]
        public void FinishUp_Success_When_Status_Is_Active_Or_Overdue(ContractStatus status)
        {
            var contract = ContractBuilder.Build(1, status: status);

            contract.FinishUp(DateTime.UtcNow, contract.StartMileage);

            contract.Status.ShouldBe(ContractStatus.Finished);
        }

        // 1.1 — Estados a partir dos quais o FinishUp NÃO pode acontecer.
        [Theory]
        [InlineData(ContractStatus.Reserved)]
        [InlineData(ContractStatus.Cancelled)]
        [InlineData(ContractStatus.Finished)]
        [InlineData(ContractStatus.Renewed)]
        public void FinishUp_Error_When_Status_Is_Not_Active_Or_Overdue(ContractStatus status)
        {
            var contract = ContractBuilder.Build(1, status: status);

            var exception = Should.Throw<BusinessRuleException>(
                () => contract.FinishUp(DateTime.UtcNow, contract.StartMileage));

            exception.Message.ShouldBe(ResourceErrorMessages.CONTRACT_NOT_ACTIVE);
        }

        // 1.4 — Mileage: limites.
        [Fact]
        public void FinishUp_Error_When_FinalMileage_Less_Than_StartMileage()
        {
            var contract = ContractBuilder.Build(1, status: ContractStatus.Active);

            var exception = Should.Throw<BusinessRuleException>(
                () => contract.FinishUp(DateTime.UtcNow, contract.StartMileage - 1));

            exception.Message.ShouldBe(ResourceErrorMessages.END_MILEAGE_CANNOT_BE_LESS_THAN_START);
        }

        [Fact]
        public void FinishUp_ExcessMileageFee_Is_Zero_When_FinalMileage_Equals_StartMileage()
        {
            var contract = ContractBuilder.Build(1, status: ContractStatus.Active);

            contract.FinishUp(DateTime.UtcNow, contract.StartMileage);

            contract.ExcessMileageFee.ShouldBe(0);
        }

        [Fact]
        public void FinishUp_ExcessMileageFee_Is_Zero_When_FinalMileage_Equals_Contracted_Limit()
        {
            // FinalMileage = StartMileage + MileageContracted deve ficar exatamente na borda,
            // sem gerar cobrança de excesso.
            var contract = ContractBuilder.Build(1, status: ContractStatus.Active);

            contract.FinishUp(DateTime.UtcNow, contract.StartMileage + contract.MileageContracted);

            contract.ExcessMileageFee.ShouldBe(0);
        }

        [Fact]
        public void FinishUp_ExcessMileageFee_Is_Charged_When_FinalMileage_Exceeds_Contracted_Limit()
        {
            var contract = ContractBuilder.Build(1, status: ContractStatus.Active);
            var finalMileage = contract.StartMileage + contract.MileageContracted + 50;

            contract.FinishUp(DateTime.UtcNow, finalMileage);

            contract.ExcessMileageFee.ShouldBe(50 * contract.SnapshotPricePerExtraMileage);
        }

        // 1.3 — Limites de tempo: qualquer fração de dia já iniciada conta como um dia cheio de atraso.
        [Theory]
        [InlineData(0, 0)]   // devolvido exatamente no horário previsto
        [InlineData(1, 1)]   // +1 hora
        [InlineData(23, 1)]  // +23 horas
        [InlineData(24, 1)]  // +24 horas
        [InlineData(25, 2)]  // +25 horas
        public void CalculateDaysLate_Rounds_Any_Started_Day_Up(double hoursLate, int expectedDaysLate)
        {
            var returnDueDateTime = new DateTime(2026, 1, 1, 12, 0, 0, DateTimeKind.Utc);
            var actualReturnDateTime = returnDueDateTime.AddHours(hoursLate);

            var daysLate = Contract.CalculateDaysLate(returnDueDateTime, actualReturnDateTime);

            daysLate.ShouldBe(expectedDaysLate);
        }

        [Fact]
        public void CalculateDaysLate_Rounds_Any_Fraction_Past_A_Full_Day_Up()
        {
            var returnDueDateTime = new DateTime(2026, 1, 1, 12, 0, 0, DateTimeKind.Utc);
            var actualReturnDateTime = returnDueDateTime.AddHours(24).AddSeconds(1);

            var daysLate = Contract.CalculateDaysLate(returnDueDateTime, actualReturnDateTime);

            daysLate.ShouldBe(2);
        }

        [Fact]
        public void CalculateDaysLate_Returns_Zero_When_Returned_Before_Due_Date()
        {
            var returnDueDateTime = new DateTime(2026, 1, 1, 12, 0, 0, DateTimeKind.Utc);
            var actualReturnDateTime = returnDueDateTime.AddHours(-1);

            var daysLate = Contract.CalculateDaysLate(returnDueDateTime, actualReturnDateTime);

            daysLate.ShouldBe(0);
        }
    }
}
