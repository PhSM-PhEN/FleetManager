using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using FleetManager.Application.UseCase.ToUser.GetProfile;
using FleetManager.Domain.Entities;
using Shouldly;
namespace UseCase.Tests.ToUser.GetProfile
{
    public class GetProfileUserUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var user = UserBuilder.Build();

            var useCase = CreateUsecase(user);
            var result = await useCase.Execute();

            result.ShouldNotBeNull();
            result.Name.ShouldBe(user.Name);
            result.Email.ShouldBe(user.Email);
        }

       

        private GetProfileUserUseCase CreateUsecase(User user)
        {
            var loggedUser = LoggedUserBuilder.Build(user);
            var repository = new UserReadOnlyRepositoryBuilder()
                .GetById(user)
                .Build();

            return new GetProfileUserUseCase(repository, loggedUser);
        }
    }
}