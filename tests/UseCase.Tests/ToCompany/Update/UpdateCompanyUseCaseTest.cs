using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Request.ToCompany;
using FleetManager.Application.UseCase.ToCompany.Update;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.ToCompany.Update
{
    public class UpdateCompanyUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var address = AddressBuilder.Build();
            var company = CompanyBuilder.Build(addressId: address.Id);
            var request = RequestCompanyJsonBuilder.Build(address.Id);

            var useCase = CreateUseCase(company, address);

            await useCase.Execute(company.Id, request);
        }

        [Fact]
        public async Task Error_Company_Not_Found()
        {
            var address = AddressBuilder.Build();
            var request = RequestCompanyJsonBuilder.Build(address.Id);

            var useCase = CreateUseCase(company: null, address: address, companyId: 999);
            var act = async () => await useCase.Execute(999, request);

            var result = await act.ShouldThrowAsync<NotFoundException>();
            result.Message.ShouldBe(ResourceErrorMessages.COMPANY_NOT_FOUND);
        }

        [Fact]
        public async Task Error_Address_Not_Found()
        {
            var company = CompanyBuilder.Build();
            var request = RequestCompanyJsonBuilder.Build(addressId: 999);

            var useCase = CreateUseCase(company, address: null);
            var act = async () => await useCase.Execute(company.Id, request);

            await act.ShouldThrowAsync<NotFoundException>();
        }

        private static UpdateCompanyUseCase CreateUseCase(
            FleetManager.Domain.Entities.Company? company,
            FleetManager.Domain.Entities.Address? address,
            long? companyId = null)
        {
            var repository = new CompanyWriteOnlyRepositoryBuilder()
                .GetById(company, companyId ?? company?.Id ?? 0)
                .Build();

            var addressReadOnly = new AddressReadOnlyRepositoryBuilder()
                .GetById(address?.Id ?? 999, address)
                .Build();

            var unitOfWork = UnitOfWorkBuilder.Build();

            return new UpdateCompanyUseCase(repository, addressReadOnly, unitOfWork);
        }
    }
}