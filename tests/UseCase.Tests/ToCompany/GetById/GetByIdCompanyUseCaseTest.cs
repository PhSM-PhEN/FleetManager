using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using FleetManager.Application.UseCase.ToCompany.GetById;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.ToCompany.GetById
{
    public class GetByIdCompanyUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var address = AddressBuilder.Build(1);
            var company = CompanyBuilder.Build(addressId: address.Id);
            company.Address = address;

            var useCase = CreateUseCase(company);
            var result = await useCase.Execute(company.Id);

            result.ShouldNotBeNull();
            result.Name.ShouldBe(company.Name);
            result.Address.Street.ShouldBe(address.Street);
        }

        [Fact]
        public async Task Error_Company_Not_Found()
        {
            var useCase = CreateUseCase(company: null);
            var act = async () => await useCase.Execute(999);

            var result = await act.ShouldThrowAsync<NotFoundException>();
            result.Message.ShouldBe(ResourceErrorMessages.COMPANY_NOT_FOUND);
        }

        private static GetByIdCompanyUseCase CreateUseCase(FleetManager.Domain.Entities.Company? company)
        {
            var repository = new CompanyReadOnlyRepositoryBuilder()
                .GetById(company, company?.Id ?? 999)
                .Build();

            return new GetByIdCompanyUseCase(repository);
        }
    }
}