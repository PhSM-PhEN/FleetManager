using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using FleetManager.Application.UseCase.ToTenant.GetAll;
using FleetManager.Domain.Entities;
using Shouldly;

namespace UseCase.Tests.ToTenant.GetAll
{
    public class GetAllTenantUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var tenants = TenantBuilder.Collection();

            var useCase = CreateUseCase(1, 10, tenants, tenants.Count);
            var result = await useCase.Execute(1, 10);

            result.ShouldNotBeNull();
            result.Data.Count.ShouldBe(tenants.Count);
            result.PageNumber.ShouldBe(1);
            result.PageSize.ShouldBe(10);
            result.TotalCount.ShouldBe(tenants.Count);
        }

        [Fact]
        public async Task Success_Invalid_PageNumber_Defaults_To_One()
        {
            var tenants = TenantBuilder.Collection();

            var useCase = CreateUseCase(1, 10, tenants, tenants.Count);
            var result = await useCase.Execute(0, 10);

            result.PageNumber.ShouldBe(1);
        }

        [Fact]
        public async Task Success_Invalid_PageSize_Defaults_To_Ten()
        {
            var tenants = TenantBuilder.Collection();

            var useCase = CreateUseCase(1, 10, tenants, tenants.Count);
            var result = await useCase.Execute(1, 100);

            result.PageSize.ShouldBe(10);
        }

        private static GetAllTenantUseCase CreateUseCase(int pageNumber, int pageSize, List<Tenant> tenants, int totalCount)
        {
            var repository = new TenantReadOnlyRepositoryBuilder()
                .GetAll(tenants, totalCount, pageNumber, pageSize)
                .Build();

            return new GetAllTenantUseCase(repository);
        }
    }
}