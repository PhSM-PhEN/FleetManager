using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Request.ToTenant;
using FleetManager.Application.UseCase.ToTenant.Register;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Entities.ValueObjects;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.ToTenant.Register
{
    public class RegisterTenantUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var address = AddressBuilder.Build(1);
            var request = RequestTenantJsonBuilder.Build(address.Id);

            var useCase = CreateUseCase(address, cpfExists: false);
            var result = await useCase.Execute(request);

            result.ShouldNotBeNull();
            result.Name.ShouldBe(request.Name);
        }

        [Fact]
        public async Task Error_Name_Empty()
        {
            var address = AddressBuilder.Build(1);
            var request = RequestTenantJsonBuilder.Build(address.Id);
            request.Name = string.Empty;

            var useCase = CreateUseCase(address, cpfExists: false);
            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();
            result.GetErrors().ShouldContain(ResourceErrorMessages.NAME_IS_REQUIRED);
        }

        [Fact]
        public async Task Error_Address_Not_Found()
        {
            var request = RequestTenantJsonBuilder.Build(999);

            var useCase = CreateUseCase(address: null, cpfExists: false);
            var act = async () => await useCase.Execute(request);

            await act.ShouldThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task Error_Cpf_Already_Registered()
        {
            var address = AddressBuilder.Build(1);
            var request = RequestTenantJsonBuilder.Build(address.Id);
            var normalizedCpf = new Cpf(request.Cpf).Number;

            var useCase = CreateUseCase(address, cpfExists: true, cpf: normalizedCpf);
            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();
            result.GetErrors().ShouldContain(ResourceErrorMessages.CPF_ALREADY_REGISTERED);
        }

        private static RegisterTenantUseCase CreateUseCase(Address? address, bool cpfExists, string? cpf = null)
        {
            var writeRepository = new TenantWriteOnlyRepositoryBuilder().Build();

            var readRepositoryBuilder = new TenantReadOnlyRepositoryBuilder();
            if (cpf is not null)
                readRepositoryBuilder.ExistByCpf(cpf, cpfExists);
            var readRepository = readRepositoryBuilder.Build();

            var addressReadOnly = new AddressReadOnlyRepositoryBuilder()
                .GetById(address?.Id ?? 999, address)
                .Build();

            var unitOfWork = UnitOfWorkBuilder.Build();

            return new RegisterTenantUseCase(writeRepository, readRepository, addressReadOnly, unitOfWork);
        }
    }
}