using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.ToContract;
using CommonTestUtilities.Request.ToContract;
using FleetManager.Application.UseCase.ToContract.Update;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Enum;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.ToContract.Update
{
    public class UpdateContractUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var contract = BuildReservedContract(1);
            var request = RequestUpdateContractJsonBuilder.Build();

            var useCase = CreateUseCase(contract);
            await useCase.Execute(contract.Id, request);

            contract.RentalType.ShouldBe(Enum.Parse<RentalType>(request.RentalType));
        }

        [Fact]
        public async Task Error_Contract_Not_Found()
        {
            var contract = BuildReservedContract(1);
            var request = RequestUpdateContractJsonBuilder.Build();

            var useCase = CreateUseCase(contract);
            var act = async () => await useCase.Execute(999, request);

            var result = await act.ShouldThrowAsync<NotFoundException>();
            result.Message.ShouldBe(ResourceErrorMessages.CONTRACT_NOT_FOUND);
        }

        [Fact]
        public async Task Error_Contract_Not_Editable()
        {
            var contract = BuildReservedContract(1, ContractStatus.Cancelled);
            var request = RequestUpdateContractJsonBuilder.Build();

            var useCase = CreateUseCase(contract);
            var act = async () => await useCase.Execute(contract.Id, request);

            var result = await act.ShouldThrowAsync<BusinessRuleException>();
            result.Message.ShouldBe(ResourceErrorMessages.CONTRACT_NOT_EDITABLE);
        }

        [Fact]
        public async Task Error_RentalType_Invalid()
        {
            var contract = BuildReservedContract(1);
            var request = RequestUpdateContractJsonBuilder.Build();
            request.RentalType = "Weekly";

            var useCase = CreateUseCase(contract);
            var act = async () => await useCase.Execute(contract.Id, request);

            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();
            result.GetErrors().ShouldContain(ResourceErrorMessages.RENTAL_TYPE_INVALID);
        }

        [Fact]
        public async Task Error_ReturnDueDate_Required_When_Daily()
        {
            var contract = BuildReservedContract(1);
            var request = RequestUpdateContractJsonBuilder.Build();
            request.ReturnDueDateTime = null;

            var useCase = CreateUseCase(contract);
            var act = async () => await useCase.Execute(contract.Id, request);

            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();
            result.GetErrors().ShouldContain(ResourceErrorMessages.RETURN_DUE_DATE_REQUIRED);
        }

        private static Contract BuildReservedContract(long id, ContractStatus status = ContractStatus.Reserved)
        {
            var rentalPlan = RentalPlanBuilder.Build(1);
            var contract = ContractBuilder.Build(id: id, rentalPlan: rentalPlan, status: status);
            contract.RentalPlan = rentalPlan;

            return contract;
        }

        private static UpdateContractUseCase CreateUseCase(Contract contract)
        {
            var repository = new ContractWriteOnlyRepositoryBuilder()
                .GetById(contract.Id, contract)
                .Update(contract)
                .Build();

            var unitOfWork = UnitOfWorkBuilder.Build();

            return new UpdateContractUseCase(repository, unitOfWork);
        }
    }
}
