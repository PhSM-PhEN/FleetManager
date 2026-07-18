using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using FleetManager.Application.UseCase.ToAddress.Delete;
using FleetManager.Domain.Entities;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.ToAddress.Delete
{
    public class DeleteAddressUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var address = AddressBuilder.Build(1);

            var useCase = CreateUseCase(address);
            var act = async () => { await useCase.Execute(address.Id); };

            await act.ShouldNotThrowAsync();
        }

        [Fact]
        public async Task Error_Address_Not_Found()
        {
            var repository = new AddressWriteOnlyRepositoryBuilder().Build();
            var unitOfWork = UnitOfWorkBuilder.Build();

            var useCase = new DeleteAddressUseCase(repository, unitOfWork);

            var act = async () => { await useCase.Execute(1); };

            var exception = await act.ShouldThrowAsync<NotFoundException>();
            exception.GetErrors().ShouldContain(ResourceErrorMessages.ADDRESS_NOT_FOUND);
        }

        private static DeleteAddressUseCase CreateUseCase(Address address)
        {
            var repository = new AddressWriteOnlyRepositoryBuilder()
                .GetById(address.Id, address)
                .Delete(address.Id)
                .Build();
            var unitOfWork = UnitOfWorkBuilder.Build();

            return new DeleteAddressUseCase(repository, unitOfWork);
        }
    }
}
