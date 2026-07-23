using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Request.ToUser;
using CommonTestUtilities.Token;
using FleetManager.Application.UseCase.DoLogin;
using FleetManager.Communication.Request.ToUser;
using FleetManager.Domain.Entities;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.DoLogin
{
    public class DoLoginUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var user = UserBuilder.Build();
            var request = new RequestLoginUserJson { Email = user.Email, Password = "aA1validPassword" };

            var useCase = CreateUseCase(user, correctPassword: request.Password);
            var result = await useCase.Execute(request);

            result.ShouldNotBeNull();
            result.Name.ShouldBe(user.Name);
            result.Token.ShouldNotBeNullOrEmpty();
        }

        [Fact]
        public async Task Error_Email_Not_Found()
        {
            var request = RequestLoginUserJsonBuilder.Build();

            var useCase = CreateUseCase(user: null, correctPassword: null, email: request.Email);
            var act = async () => await useCase.Execute(request);

            var exception = await act.ShouldThrowAsync<InvalidLoginException>();
            exception.GetErrors().ShouldContain(ResourceErrorMessages.EMAIL_OR_PASSWORD_INVALID);
        }

        [Fact]
        public async Task Error_Wrong_Password()
        {
            var user = UserBuilder.Build();
            var request = new RequestLoginUserJson { Email = user.Email, Password = "wrongPassword1A" };

            var useCase = CreateUseCase(user, correctPassword: "aA1validPassword");
            var act = async () => await useCase.Execute(request);

            var exception = await act.ShouldThrowAsync<InvalidLoginException>();
            exception.GetErrors().ShouldContain(ResourceErrorMessages.EMAIL_OR_PASSWORD_INVALID);
        }

        private static DoLoginUseCase CreateUseCase(User? user, string? correctPassword, string? email = null)
        {
            var readRepositoryBuilder = new UserReadOnlyRepositoryBuilder();

            if (user is not null)
                readRepositoryBuilder.GetUserByEmail(user);
            else
                readRepositoryBuilder.GetUserByEmail(email ?? string.Empty, null);

            var readRepository = readRepositoryBuilder.Build();

            var passwordEncrypterBuilder = new PasswordEncrypterBuilder();
            if (correctPassword is not null)
                passwordEncrypterBuilder.Verify(correctPassword);

            var passwordEncrypter = passwordEncrypterBuilder.Build();
            var tokenGenerator = JwtTokenGeneratorBuilder.Build();

            return new DoLoginUseCase(readRepository, passwordEncrypter, tokenGenerator);
        }
    }
}