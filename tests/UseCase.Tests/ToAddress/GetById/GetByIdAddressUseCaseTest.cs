using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using FleetManager.Application.UseCase.ToAddress.GetById;
using FleetManager.Domain.Entities;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.ToAddress.GetById
{
    public class GetByIdAddressUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var address = AddressBuilder.Build(1);

            var useCase = CreateUseCase(address);
            var result = await useCase.Execute(address.Id);

            result.ShouldNotBeNull();
            result.Street.ShouldBe(address.Street);
            result.Number.ShouldBe(address.Number);
            result.City.ShouldBe(address.City);
            result.State.ShouldBe(address.State);
            result.ZipCode.ShouldBe(address.ZipCode);
        }

        [Fact]
        public async Task Error_Address_Not_Found()
        {
            var repository = new AddressReadOnlyRepositoryBuilder()
                .GetById(1, null)
                .Build();

            var useCase = new GetByIdAddressUseCase(repository);

            var act = async () => await useCase.Execute(1);

            var exception = await act.ShouldThrowAsync<NotFoundException>();
            exception.GetErrors().ShouldContain(ResourceErrorMessages.ADDRESS_NOT_FOUND);
        }

        private static GetByIdAddressUseCase CreateUseCase(Address address)
        {
            var repository = new AddressReadOnlyRepositoryBuilder()
                .GetById(address.Id, address)
                .Build();

            return new GetByIdAddressUseCase(repository);
        }
    }
}
