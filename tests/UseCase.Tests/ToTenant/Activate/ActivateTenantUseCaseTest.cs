using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.ToTenant;
using FleetManager.Application.UseCase.ToTenant.Activate;
using FleetManager.Domain.Entities;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.ToTenant.Activate
{
    public class ActivateTenantUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var tenant = TenantBuilder.Build(1);
            tenant.Deactivate();

            var useCase = CreateUseCase(tenant);
            var act = async () => await useCase.Execute(tenant.Id);

            await act.ShouldNotThrowAsync();
        }

        [Fact]
        public async Task Error_Tenant_Not_Found()
        {
            var useCase = CreateUseCase(tenant: null);
            var act = async () => await useCase.Execute(999);

            var result = await act.ShouldThrowAsync<NotFoundException>();
            result.Message.ShouldBe(ResourceErrorMessages.TENANT_NOT_FOUND);
        }

        [Fact]
        public async Task Error_Already_Active()
        {
            var tenant = TenantBuilder.Build(1);

            var useCase = CreateUseCase(tenant);
            var act = async () => await useCase.Execute(tenant.Id);

            var result = await act.ShouldThrowAsync<BusinessRuleException>();
            result.Message.ShouldBe(ResourceErrorMessages.TENANT_ALREADY_ACTIVE);
        }

        private static ActivateTenantUseCase CreateUseCase(Tenant? tenant)
        {
            var repositoryBuilder = new TenantWriteOnlyRepositoryBuilder();

            if (tenant is not null)
                repositoryBuilder.GetById(tenant, tenant.Id);

            var repository = repositoryBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();

            return new ActivateTenantUseCase(repository, unitOfWork);
        }
    }
}
