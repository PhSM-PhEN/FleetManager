using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Request.ToCompany;
using FleetManager.Application.UseCase.ToCompany.Register;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.ToCompany.Register
{
    public class RegisterCompanyUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var address = AddressBuilder.Build(1);
            var request = RequestCompanyJsonBuilder.Build(address.Id);

            var useCase = CreateUseCase(address: address, cnpjExists: false);
            var result = await useCase.Execute(request);

            result.ShouldNotBeNull();
            result.Name.ShouldBe(request.Name);
        }

        [Fact]
        public async Task Error_Name_Empty()
        {
            var address = AddressBuilder.Build(1);
            var request = RequestCompanyJsonBuilder.Build(address.Id);
            request.Name = string.Empty;

            var useCase = CreateUseCase(address: address, cnpjExists: false);
            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();
            result.GetErrors().ShouldContain(ResourceErrorMessages.NAME_IS_REQUIRED);
        }

        [Fact]
        public async Task Error_Address_Not_Found()
        {
            var request = RequestCompanyJsonBuilder.Build(addressId: 999);

            var useCase = CreateUseCase(address: null, cnpjExists: false);
            var act = async () => await useCase.Execute(request);

            await act.ShouldThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task Error_Cnpj_Already_Registered()
        {
            var address = AddressBuilder.Build(1);
            var request = RequestCompanyJsonBuilder.Build(address.Id);

            var useCase = CreateUseCase(address: address, cnpjExists: true, cnpj: request.Cnpj);
            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();
            result.GetErrors().ShouldContain(ResourceErrorMessages.CNPJ_ALREADY_REGISTERED);
        }

        private static RegisterCompanyUseCase CreateUseCase(
            FleetManager.Domain.Entities.Address? address,
            bool cnpjExists,
            string? cnpj = null)
        {
            var writeRepository = new CompanyWriteOnlyRepositoryBuilder().Build();

            var readRepositoryBuilder = new CompanyReadOnlyRepositoryBuilder();
            if (cnpj is not null)
                readRepositoryBuilder.ExistByCnpj(cnpj, cnpjExists);
            var readRepository = readRepositoryBuilder.Build();

            var addressReadOnly = new AddressReadOnlyRepositoryBuilder()
                .GetById(address?.Id ?? 999, address)
                .Build();

            var unitOfWork = UnitOfWorkBuilder.Build();

            return new RegisterCompanyUseCase(writeRepository, readRepository, addressReadOnly, unitOfWork);
        }
    }
}