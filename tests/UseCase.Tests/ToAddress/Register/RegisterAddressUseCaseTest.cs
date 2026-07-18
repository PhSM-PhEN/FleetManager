using CommonTestUtilities.Repositories;
using CommonTestUtilities.Request.ToAddress;
using FleetManager.Application.UseCase.ToAddress.Register;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.ToAddress.Register
{
    public class RegisterAddressUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var request = RequestAddressJsonBuilder.Build();
            var useCase = CreateUseCase();
            var result = await useCase.Execute(request);

            result.ShouldNotBeNull();
            result.Street.ShouldBe(request.Street);

        }
        [Fact]
        public async Task Error_Street_Empty()
        {
            var request = RequestAddressJsonBuilder.Build();
            request.Street = string.Empty;
            var useCase = CreateUseCase();
            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();
            result.GetErrors().ShouldContain(ResourceErrorMessages.STREET_REQUIRED);
        }
        private static RegisterAddressUseCase CreateUseCase ()
        {
            
            var repository = new AddressWriteOnlyRepositoryBuilder().Build();
            var unitOfWork = UnitOfWorkBuilder.Build();
            return new RegisterAddressUseCase(repository, unitOfWork);
            
        }
    }
}
