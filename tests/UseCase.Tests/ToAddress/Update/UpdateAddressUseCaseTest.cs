using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Request.ToAddress;
using FleetManager.Application.UseCase.ToAddress.Update;
using FleetManager.Domain.Entities;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.ToAddress.Update
{
    public class UpdateAddressUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var address = AddressBuilder.Build(1);
            var request = RequestAddressJsonBuilder.Build();

            var useCase = CreateUseCase(address);
            var act = async () => await useCase.Execute(address.Id, request);

            await act.ShouldNotThrowAsync();
            address.Street.ShouldBe(request.Street);
            address.Number.ShouldBe(request.Number);
            address.City.ShouldBe(request.City);
            address.State.ShouldBe(request.State);
            address.ZipCode.ShouldBe(request.ZipCode);
        }

        [Fact]
        public async Task Error_Address_Not_Found()
        {
            var request = RequestAddressJsonBuilder.Build();
            var repository = new AddressWriteOnlyRepositoryBuilder().Build();
            var unitOfWork = UnitOfWorkBuilder.Build();

            var useCase = new UpdateAddressUseCase(repository, unitOfWork);

            var act = async () => await useCase.Execute(1, request);

            var exception = await act.ShouldThrowAsync<NotFoundException>();
            exception.GetErrors().ShouldContain(ResourceErrorMessages.ADDRESS_NOT_FOUND);
        }

        [Fact]
        public async Task Error_Street_Empty()
        {
            var address = AddressBuilder.Build(1);
            var request = RequestAddressJsonBuilder.Build();
            request.Street = string.Empty;

            var useCase = CreateUseCase(address);
            var act = async () => await useCase.Execute(address.Id, request);

            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();
            result.GetErrors().ShouldContain(ResourceErrorMessages.STREET_REQUIRED);
        }

        [Fact]
        public async Task Error_ZipCode_Invalid()
        {
            var address = AddressBuilder.Build(1);
            var request = RequestAddressJsonBuilder.Build();
            request.ZipCode = "invalid-zip";

            var useCase = CreateUseCase(address);
            var act = async () => await useCase.Execute(address.Id, request);

            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();
            result.GetErrors().ShouldContain(ResourceErrorMessages.ZIPCODE_INVALID);
        }

        private static UpdateAddressUseCase CreateUseCase(Address address)
        {
            var repository = new AddressWriteOnlyRepositoryBuilder()
                .GetById(address.Id, address)
                .Update(address)
                .Build();
            var unitOfWork = UnitOfWorkBuilder.Build();

            return new UpdateAddressUseCase(repository, unitOfWork);
        }
    }
}
