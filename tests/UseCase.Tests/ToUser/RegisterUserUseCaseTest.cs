using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Request.ToUser;
using CommonTestUtilities.Token;
using FleetManager.Application.UseCase.ToUser.Register;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.ToUser
{
    public class RegisterUserUseCaseTest
    {
        [Fact]
        public async Task Sucess()
        {
            
                 

                 
            
            var request = RequestRegisterUserJsonBuilder.Build();

            var useCase = CreateUseCase();

            var result = await useCase.Execute(request);

            result.ShouldNotBeNull();
            result.Name.ShouldBe(request.Name);
            result.Token.ShouldNotBeNullOrEmpty();
        }
        [Fact]
        public async Task Error_Name_Empty()
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Name = string.Empty;

            var useCase = CreateUseCase();


            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();

            result.GetErrors().ShouldContain(ResourceErrorMessages.NAME_IS_REQUIRED);


        }

        [Fact]
        public async Task Error_Email_Already_Registered()
        {
            var request = RequestRegisterUserJsonBuilder.Build();

            var useCase = CreateUseCase(request.Email);

            var act = async () => await useCase.Execute(request);
            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();

            result.GetErrors().ShouldContain(ResourceErrorMessages.EMAIL_ALREADY_REGISTERED);


        }
        private static RegisterUserUseCase CreateUseCase(string? email = null)
        {
            var unitOfWork = UnitOfWorkBuilder.Build();
            var readRepositoryBuilder = new UserReadOnlyRepositoryBuilder();
            var writeRepository = new UserWriteOnlyRepositoryBuilder().Build();
            var passwordEncripter = new PasswordEncrypterBuilder().Build();

            var token = JwtTokenGeneratorBuilder.Build();

            if (string.IsNullOrEmpty(email) == false)
            {
                readRepositoryBuilder.ExistByEmail(email);
            }

            var readRepository = readRepositoryBuilder.Build();

            return new RegisterUserUseCase(unitOfWork, readRepository, writeRepository, passwordEncripter, token);
        }

    }
}
