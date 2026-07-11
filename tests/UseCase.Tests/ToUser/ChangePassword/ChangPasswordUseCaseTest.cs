using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Request.ToUser;
using FleetManager.Application.UseCase.ToUser.ChangePassword;
using FleetManager.Domain.Entities;
using Shouldly;

namespace UseCase.Tests.ToUser.ChangePassword
{
    public class ChangPasswordUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            
            var request = RequestChangPasswordJsonBuilder.Build();
            var user = UserBuilder.Build();
            var useCase = CreateUseCase(user, request.OldPassword);

            var act = async () => await useCase.Execute(request);
            await act.ShouldNotThrowAsync();

        }

        private ChangPasswordUseCase CreateUseCase(User user, string? password = null)
        {
            var unitOfWork = UnitOfWorkBuilder.Build();
            var logged = LoggedUserBuilder.Build(user);
            var encrypter = new PasswordEncrypterBuilder().Verify(password).Build();

            var repository = new UserWriteOnlyRepositoryBuilder()
               .GetUserById(user)
               .Update(user)
               .Build();

            return new ChangPasswordUseCase(logged, encrypter, repository,  unitOfWork);

        }
    }
}
