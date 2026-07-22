using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
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
            var repository = new CompanyReadOnlyRepositoryBuilder()
                .GetAll(companies)
                .Build();

            var useCase = new GetAllCompanyUseCase(repository);
            var result = await useCase.Execute();

            result.ShouldNotBeNull();
            result.Count.ShouldBe(companies.Count);
        }

        [Fact]
        public async Task Success_Empty_List()
        {
            var repository = new CompanyReadOnlyRepositoryBuilder()
                .GetAll([])
                .Build();

            var useCase = new GetAllCompanyUseCase(repository);
            var result = await useCase.Execute();

            result.ShouldNotBeNull();
            result.ShouldBeEmpty();
        }
    }
}