
using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Request.ToAddress;
using FleetManager.Application.UseCase.ToAddress.Register;
using FleetManager.Domain.Entities;
using Shouldly;

namespace UseCase.Tests.ToAddress
{
    public class RegisterAddressUseCaseTest
    {
        [Fact]
        public async Task Succes()
        {
            var request = RequestAddressJsonBuilder.Build();
            var useCase = CreateUseCase();
            var result = await useCase.Execute(request);

            result.ShouldNotBeNull();

        }
        private static RegisterAddressUseCase CreateUseCase ()
        {
            
            var repository = new AddressWriteOnlyRepositoryBuilder().Build();
            var unitOfWork = UnitOfWorkBuilder.Build();
            return new RegisterAddressUseCase(repository, unitOfWork);
            
        }
    }
}
