using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Request.ToTenant;
using FleetManager.Application.UseCase.ToTenant.Update;
using FleetManager.Domain.Entities;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.ToTenant.Update
{
    public class UpdateTenantUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var address = AddressBuilder.Build(1);
            var tenant = TenantBuilder.Build(1, addressId: address.Id);
            var request = RequestUpdateTenantJsonBuilder.Build(address.Id);

            var useCase = CreateUseCase(tenant, address);
            var act = async () => await useCase.Execute(tenant.Id, request);

            await act.ShouldNotThrowAsync();
            tenant.Contact.PhoneNumber.ShouldBe(request.PhoneNumber);
        }

        [Fact]
        public async Task Error_Tenant_Not_Found()
        {
            var address = AddressBuilder.Build(1);
            var request = RequestUpdateTenantJsonBuilder.Build(address.Id);

            var useCase = CreateUseCase(tenant: null, address: address, tenantId: 999);
            var act = async () => await useCase.Execute(999, request);

            var result = await act.ShouldThrowAsync<NotFoundException>();
            result.Message.ShouldBe(ResourceErrorMessages.TENANT_NOT_FOUND);
        }

        [Fact]
        public async Task Error_Address_Not_Found()
        {
            var tenant = TenantBuilder.Build(1);
            var request = RequestUpdateTenantJsonBuilder.Build(999);

            var useCase = CreateUseCase(tenant, address: null);
            var act = async () => await useCase.Execute(tenant.Id, request);

            await act.ShouldThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task Error_PhoneNumber_Empty()
        {
            var address = AddressBuilder.Build(1);
            var tenant = TenantBuilder.Build(1, addressId: address.Id);
            var request = RequestUpdateTenantJsonBuilder.Build(address.Id);
            request.PhoneNumber = string.Empty;

            var useCase = CreateUseCase(tenant, address);
            var act = async () => await useCase.Execute(tenant.Id, request);

            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();
            result.GetErrors().ShouldContain(ResourceErrorMessages.PHONE_NUMBER_REQUIRED);
        }

        private static UpdateTenantUseCase CreateUseCase(Tenant? tenant, Address? address, long? tenantId = null)
        {
            var writeBuilder = new TenantWriteOnlyRepositoryBuilder()
                .GetById(tenant, tenantId ?? tenant?.Id ?? 999);

            if (tenant is not null)
                writeBuilder.Update(tenant);

            var repository = writeBuilder.Build();

            var addressReadOnly = new AddressReadOnlyRepositoryBuilder()
                .GetById(address?.Id ?? 999, address)
                .Build();

            var unitOfWork = UnitOfWorkBuilder.Build();

            return new UpdateTenantUseCase(repository, addressReadOnly, unitOfWork);
        }
    }
}