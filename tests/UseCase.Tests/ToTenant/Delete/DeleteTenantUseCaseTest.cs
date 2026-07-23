using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using FleetManager.Application.UseCase.ToTenant.Delete;
using FleetManager.Domain.Entities;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.ToTenant.Delete
{
    public class DeleteTenantUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var tenant = TenantBuilder.Build(1);
            var useCase = CreateUseCase(tenant);

            await useCase.Execute(tenant.Id);
        }

        [Fact]
        public async Task Error_Tenant_Not_Found()
        {
            var useCase = CreateUseCase(tenant: null);
            var act = async () => await useCase.Execute(999);

            var result = await act.ShouldThrowAsync<NotFoundException>();
            result.Message.ShouldBe(ResourceErrorMessages.TENANT_NOT_FOUND);
        }

        private static DeleteTenantUseCase CreateUseCase(Tenant? tenant)
        {
            var repository = new TenantWriteOnlyRepositoryBuilder()
                .GetById(tenant, tenant?.Id ?? 999)
                .Delete(tenant?.Id ?? 999)
                .Build();

            var unitOfWork = UnitOfWorkBuilder.Build();

            return new DeleteTenantUseCase(repository, unitOfWork);
        }
    }
}