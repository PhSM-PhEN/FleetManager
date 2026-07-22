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
            var address = AddressBuilder.Build();
            var company = CompanyBuilder.Build(addressId: address.Id);
            company.Address = address;

            var repository = new CompanyReadOnlyRepositoryBuilder()
                .GetById(company, company.Id)
                .Build();

            var useCase = new GetByIdCompanyUseCase(repository);
            var result = await useCase.Execute(company.Id);

            result.ShouldNotBeNull();
            result.Name.ShouldBe(company.Name);
            result.Address.Street.ShouldBe(address.Street);
        }

        [Fact]
        public async Task Error_Company_Not_Found()
        {
            var repository = new CompanyReadOnlyRepositoryBuilder()
                .GetById(null, 999)
                .Build();

            var useCase = new GetByIdCompanyUseCase(repository);
            var act = async () => await useCase.Execute(999);

            var result = await act.ShouldThrowAsync<NotFoundException>();
            result.Message.ShouldBe(ResourceErrorMessages.COMPANY_NOT_FOUND);
        }
    }
}