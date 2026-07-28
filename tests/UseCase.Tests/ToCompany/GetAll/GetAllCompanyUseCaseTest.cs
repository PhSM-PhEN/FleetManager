using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories.ToCompany;
using FleetManager.Application.UseCase.ToCompany.GetAll;
using Shouldly;

namespace UseCase.Tests.ToCompany.GetAll
{
    public class GetAllCompanyUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var companies = CompanyBuilder.Collection();

            var useCase = CreateUseCase(companies);
            var result = await useCase.Execute();

            result.ShouldNotBeNull();
            result.Count.ShouldBe(companies.Count);
        }

        [Fact]
        public async Task Success_Empty_List()
        {
            var useCase = CreateUseCase([]);
            var result = await useCase.Execute();

            result.ShouldNotBeNull();
            result.ShouldBeEmpty();
        }

        private static GetAllCompanyUseCase CreateUseCase(List<FleetManager.Domain.Entities.Company> companies)
        {
            var repository = new CompanyReadOnlyRepositoryBuilder()
                .GetAll(companies)
                .Build();

            return new GetAllCompanyUseCase(repository);
        }
    }
}