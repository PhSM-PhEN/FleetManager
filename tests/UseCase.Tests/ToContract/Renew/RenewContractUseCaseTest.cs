using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.ToContract;
using CommonTestUtilities.Repositories.ToRentalPlan;
using CommonTestUtilities.Request.ToContract;
using FleetManager.Application.UseCase.ToContract.Renew;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Enum;
using FleetManager.Domain.EnumExtensions;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.ToContract.Renew
{
    public class RenewContractUseCaseTest
    {
        [Fact]
        public async Task Success_Keeping_Same_Plan()
        {
            var plan = RentalPlanBuilder.Build(1);
            var contract = ContractBuilder.Build(1, vehicleId: 10, tenantId: 20, rentalPlan: plan, status: ContractStatus.Active);
            var request = RequestRenewContractJsonBuilder.Build();

            // Sem override: o use case busca de novo o MESMO plano do contrato (RentalPlanId),
            // já que não confia mais na navegação em memória (contract.RentalPlan).
            var useCase = CreateUseCase(contract, existingPlan: plan, newPlan: null);

            var response = await useCase.Execute(contract.Id, request);

            contract.ContractStatus.ShouldBe(ContractStatus.Renewed);
            response.ContractStatus.ShouldBe(ContractStatus.Active.ContractStatusToString());
            response.PickupDateTime.ShouldBe(contract.ReturnDueDateTime);
        }

        [Fact]
        public async Task Success_With_New_Plan()
        {
            var plan = RentalPlanBuilder.Build(1);
            var newPlan = RentalPlanBuilder.Build(2);
            var contract = ContractBuilder.Build(1, vehicleId: 10, tenantId: 20, rentalPlan: plan, status: ContractStatus.Active);
            var request = RequestRenewContractJsonBuilder.Build(newRentalPlanId: newPlan.Id);

            var useCase = CreateUseCase(contract, existingPlan: plan, newPlan: newPlan);

            var response = await useCase.Execute(contract.Id, request);

            contract.ContractStatus.ShouldBe(ContractStatus.Renewed);
            response.TotalAmount.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task Success_Renews_Overdue_Contract_Keeping_Original_Due_Date_As_Pickup()
        {
            var plan = RentalPlanBuilder.Build(1);
            var contract = ContractBuilder.Build(1, vehicleId: 10, tenantId: 20, rentalPlan: plan, status: ContractStatus.Overdue);
            var request = RequestRenewContractJsonBuilder.Build();

            var useCase = CreateUseCase(contract, existingPlan: plan, newPlan: null);

            var response = await useCase.Execute(contract.Id, request);

            contract.ContractStatus.ShouldBe(ContractStatus.Renewed);
            response.ContractStatus.ShouldBe(ContractStatus.Active.ContractStatusToString());
            // Mesmo atrasado (renovação processada depois do vencimento), a nova contagem começa
            // exatamente no antigo ReturnDueDateTime, não no momento em que a renovação foi feita.
            // A multa por atraso não é cobrada aqui — só existe se o carro for devolvido (Complete).
            response.PickupDateTime.ShouldBe(contract.ReturnDueDateTime);
        }

        [Fact]
        public async Task Success_Renews_Overdue_Contract_Within_Max_Overdue_Days()
        {
            var plan = RentalPlanBuilder.Build(1);
            // 71h de atraso -> arredonda para 3 dias (limite máximo ainda permitido).
            var contract = BuildOverdueContract(plan, hoursLate: 71);
            var request = RequestRenewContractJsonBuilder.Build();

            var useCase = CreateUseCase(contract, existingPlan: plan, newPlan: null);

            var response = await useCase.Execute(contract.Id, request);

            contract.ContractStatus.ShouldBe(ContractStatus.Renewed);
            response.ContractStatus.ShouldBe(ContractStatus.Active.ContractStatusToString());
        }

        [Fact]
        public async Task Error_Renew_Blocked_When_Overdue_Past_Max_Days()
        {
            var plan = RentalPlanBuilder.Build(1);
            // 73h de atraso -> arredonda para 4 dias, acima do limite de 3.
            var contract = BuildOverdueContract(plan, hoursLate: 73);
            var request = RequestRenewContractJsonBuilder.Build();

            var useCase = CreateUseCase(contract, existingPlan: plan, newPlan: null);
            var act = async () => await useCase.Execute(contract.Id, request);

            var result = await act.ShouldThrowAsync<BusinessRuleException>();
            result.Message.ShouldBe(ResourceErrorMessages.RENEWAL_NOT_ALLOWED_PAST_MAX_OVERDUE_DAYS);
            contract.ContractStatus.ShouldBe(ContractStatus.Overdue);
        }

        [Fact]
        public async Task Error_Contract_Not_Found()
        {
            var useCase = CreateUseCase(contract: null, existingPlan: null, newPlan: null);
            var request = RequestRenewContractJsonBuilder.Build();

            var act = async () => await useCase.Execute(999, request);

            var result = await act.ShouldThrowAsync<NotFoundException>();
            result.Message.ShouldBe(ResourceErrorMessages.CONTRACT_NOT_FOUND);
        }

        [Fact]
        public async Task Error_Contract_Not_Active()
        {
            var plan = RentalPlanBuilder.Build(1);
            var contract = ContractBuilder.Build(1, vehicleId: 10, tenantId: 20, rentalPlan: plan, status: ContractStatus.Cancelled);
            var request = RequestRenewContractJsonBuilder.Build();

            var useCase = CreateUseCase(contract, existingPlan: plan, newPlan: null);
            var act = async () => await useCase.Execute(contract.Id, request);

            var result = await act.ShouldThrowAsync<BusinessRuleException>();
            result.Message.ShouldBe(ResourceErrorMessages.CONTRACT_NOT_ACTIVE);
        }

        [Fact]
        public async Task Error_New_RentalPlan_Not_Found()
        {
            var plan = RentalPlanBuilder.Build(1);
            var contract = ContractBuilder.Build(1, vehicleId: 10, tenantId: 20, rentalPlan: plan, status: ContractStatus.Active);
            var request = RequestRenewContractJsonBuilder.Build(newRentalPlanId: 999);

            var useCase = CreateUseCase(contract, existingPlan: plan, newPlan: null);
            var act = async () => await useCase.Execute(contract.Id, request);

            var result = await act.ShouldThrowAsync<NotFoundException>();
            result.Message.ShouldBe(ResourceErrorMessages.RENTAL_PLAN_NOT_FOUND);
        }

        [Fact]
        public async Task Error_MileageContracted_Invalid()
        {
            var plan = RentalPlanBuilder.Build(1);
            var contract = ContractBuilder.Build(1, vehicleId: 10, tenantId: 20, rentalPlan: plan, status: ContractStatus.Active);
            var request = RequestRenewContractJsonBuilder.Build(mileageContracted: -1);

            var useCase = CreateUseCase(contract, existingPlan: plan, newPlan: null);
            var act = async () => await useCase.Execute(contract.Id, request);

            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();
            result.GetErrors().ShouldContain(ResourceErrorMessages.MILEAGE_CONTRACTED_INVALID);
        }

        private static Contract BuildOverdueContract(RentalPlan plan, double hoursLate)
        {
            // Contrato Daily construído diretamente (sem o ContractBuilder, que gera datas aleatórias
            // sempre no futuro) para controlar exatamente há quantas horas ele venceu.
            var returnDueDateTime = DateTime.UtcNow.AddHours(-hoursLate);
            var pickupDateTime = returnDueDateTime.AddDays(-5);

            var contract = new Contract(
                vehicleId: 10,
                tenantId: 20,
                rentalPlan: plan,
                rentalType: RentalType.Daily,
                startMileage: 0,
                mileageContracted: plan.MileagePerDay * 5,
                totalAmount: plan.DailyPrice * 5,
                pickupDateTime: pickupDateTime,
                returnDueDateTime: returnDueDateTime)
            {
                Id = 1
            };

            contract.Confirm();
            contract.MarkAsOverdue();

            return contract;
        }

        private static RenewContractUseCase CreateUseCase(Contract? contract, RentalPlan? existingPlan, RentalPlan? newPlan)
        {
            var contractRepositoryBuilder = new ContractWriteOnlyRepositoryBuilder();
            var rentalPlanRepositoryBuilder = new RentalPlanReadOnlyRepositoryBuilder();

            if (contract is not null)
            {
                contractRepositoryBuilder.GetById(contract.Id, contract);
                contractRepositoryBuilder.Update(contract);
            }
            else
            {
                contractRepositoryBuilder.GetById(999, null);
            }

            // O use case sempre busca o plano pelo repositório (novo, se houver override; senão
            // o mesmo plano de origem do contrato) — nunca confia em contract.RentalPlan em memória.
            if (existingPlan is not null)
                rentalPlanRepositoryBuilder.GetById(existingPlan);

            if (newPlan is not null)
                rentalPlanRepositoryBuilder.GetById(newPlan);

            var contractRepository = contractRepositoryBuilder.Build();
            var rentalPlanRepository = rentalPlanRepositoryBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();

            return new RenewContractUseCase(contractRepository, rentalPlanRepository, unitOfWork);
        }
    }
}