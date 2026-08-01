using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories.ToContract;
using FleetManager.Application.UseCase.ToContract.GetById;
using FleetManager.Domain.Entities;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.ToContract.GetById
{
    public class GetByIdContractUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var contract = BuildContractWithNavigation(1);

            var useCase = CreateUseCase(contract);
            var result = await useCase.Execute(contract.Id);

            result.ShouldNotBeNull();
            result.Id.ShouldBe(contract.Id);
            result.TotalAmount.ShouldBe(contract.TotalAmount);
            result.MileageContracted.ShouldBe(contract.MileageContracted);
            result.Vehicle.ShouldNotBeNull();
            result.Tenant.ShouldNotBeNull();
        }

        [Fact]
        public async Task Error_Contract_Not_Found()
        {
            var contract = BuildContractWithNavigation(1);
            var useCase = CreateUseCase(contract);
            var act = async () => await useCase.Execute(999);

            var result = await act.ShouldThrowAsync<NotFoundException>();
            result.Message.ShouldBe(ResourceErrorMessages.CONTRACT_NOT_FOUND);
        }

        private static Contract BuildContractWithNavigation(long id)
        {
            var contract = ContractBuilder.Build(id);
            contract.Vehicle = VehicleBuilder.Build(1);
            contract.Tenant = TenantBuilder.Build(1);
            contract.Tenant.Address = AddressBuilder.Build(1);

            return contract;
        }

        private static GetByIdContractUseCase CreateUseCase(Contract contract)
        {
            var repository = new ContractReadOnlyRepositoryBuilder()
                .GetById(contract.Id, contract)
                .Build();

            return new GetByIdContractUseCase(repository);
        }
    }
}
