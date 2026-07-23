using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using FleetManager.Application.UseCase.ToTenant.GetById;
using FleetManager.Domain.Entities;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.ToTenant.GetById
{
    public class GetByIdTenantUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var address = AddressBuilder.Build(1);
            var tenant = TenantBuilder.Build(1, addressId: address.Id);
            tenant.Address = address;

            var useCase = CreateUseCase(tenant, tenant.Id);
            var result = await useCase.Execute(tenant.Id);

            result.ShouldNotBeNull();
            result.Name.ShouldBe(tenant.Name);
            result.Address.Street.ShouldBe(address.Street);
        }

        [Fact]
        public async Task Error_Tenant_Not_Found()
        {
            var useCase = CreateUseCase(tenant: null, tenantId: 999);
            var act = async () => await useCase.Execute(999);

            var result = await act.ShouldThrowAsync<NotFoundException>();
            result.Message.ShouldBe(ResourceErrorMessages.TENANT_NOT_FOUND);
        }

        private static GetByIdTenantUseCase CreateUseCase(Tenant? tenant, long tenantId)
        {
            var repository = new TenantReadOnlyRepositoryBuilder()
                .GetById(tenant, tenantId)
                .Build();

            return new GetByIdTenantUseCase(repository);
        }
    }
}