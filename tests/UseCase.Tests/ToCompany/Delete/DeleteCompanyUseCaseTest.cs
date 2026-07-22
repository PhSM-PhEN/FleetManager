using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using FleetManager.Application.UseCase.ToCompany.Delete;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.ToCompany.Delete
{
    public class DeleteCompanyUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var company = CompanyBuilder.Build();
            var useCase = CreateUseCase(company);

            await useCase.Execute(company.Id);
        }

        [Fact]
        public async Task Error_Company_Not_Found()
        {
            var useCase = CreateUseCase(company: null);
            var act = async () => await useCase.Execute(999);

            var result = await act.ShouldThrowAsync<NotFoundException>();
            result.Message.ShouldBe(ResourceErrorMessages.COMPANY_NOT_FOUND);
        }

        private static DeleteCompanyUseCase CreateUseCase(FleetManager.Domain.Entities.Company? company)
        {
            var repository = new CompanyWriteOnlyRepositoryBuilder()
                .GetById(company, company?.Id ?? 999)
                .Delete(company?.Id ?? 999)
                .Build();

            var unitOfWork = UnitOfWorkBuilder.Build();

            return new DeleteCompanyUseCase(repository, unitOfWork);
        }
    }
}