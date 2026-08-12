using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.ToContract;
using FleetManager.Application.UseCase.ToContract.DetectOverdue;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Enum;
using Shouldly;

namespace UseCase.Tests.ToContract.DetectOverdue
{
    public class DetectOverdueContractsUseCaseTest
    {
        [Fact]
        public async Task Success_Marks_Active_Contracts_As_Overdue()
        {
            var overdueContract1 = ContractBuilder.Build(1, status: ContractStatus.Active);
            var overdueContract2 = ContractBuilder.Build(2, status: ContractStatus.Active);

            var useCase = CreateUseCase([overdueContract1, overdueContract2]);

            var totalMarked = await useCase.Execute();

            totalMarked.ShouldBe(2);
            overdueContract1.ContractStatus.ShouldBe(ContractStatus.Overdue);
            overdueContract2.ContractStatus.ShouldBe(ContractStatus.Overdue);
        }

        [Fact]
        public async Task Success_No_Overdue_Contracts()
        {
            var useCase = CreateUseCase([]);

            var totalMarked = await useCase.Execute();

            totalMarked.ShouldBe(0);
        }

        // 3.3 — DetectOverdue só altera o status (Active -> Overdue); não pode gerar cobrança.
        // A multa por atraso é responsabilidade exclusiva do FinishUp, calculada com base na
        // devolução real. Cobrar aqui também gerar duplicidade quando o contrato for concluído.
        [Fact]
        public async Task Success_Does_Not_Generate_Any_Charge_Or_Set_Fees()
        {
            var overdueContract = ContractBuilder.Build(1, status: ContractStatus.Active);

            var useCase = CreateUseCase([overdueContract]);

            await useCase.Execute();

            overdueContract.LateFee.ShouldBeNull();
            overdueContract.ExcessMileageFee.ShouldBeNull();
            overdueContract.FinalMileage.ShouldBeNull();
        }

        // Garantia estrutural do mesmo ponto: essa use case nem tem como cobrar, porque não
        // depende de nenhum repositório de Charge. Se algum dia alguém tentar adicionar
        // cobrança aqui, esse teste quebra e chama atenção pra revisar a decisão.
        [Fact]
        public void Constructor_Does_Not_Depend_On_Any_Charge_Repository()
        {
            var constructor = typeof(DetectOverdueContractsUseCase).GetConstructors().Single();

            var dependsOnCharge = constructor.GetParameters()
                .Any(p => p.ParameterType.Name.Contains("Charge"));

            dependsOnCharge.ShouldBeFalse();
        }

        private static DetectOverdueContractsUseCase CreateUseCase(List<Contract> overdueContracts)
        {
            var contractRepositoryBuilder = new ContractWriteOnlyRepositoryBuilder()
                .GetActiveContractsPastDueDate(overdueContracts);

            foreach (var contract in overdueContracts)
                contractRepositoryBuilder.Update(contract);

            var contractRepository = contractRepositoryBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();

            return new DetectOverdueContractsUseCase(contractRepository, unitOfWork);
        }
    }
}

