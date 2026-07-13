using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using FleetManager.Application.UseCase.ToUser.Delete;
using FleetManager.Domain.Entities;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.ToUser.Delete
{
    public class DeleteUserUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var user = UserBuilder.Build();

            var useCase = CreateUseCase(user);
            var act = async () => { await useCase.Execute(); };

            await act.ShouldNotThrowAsync();
        }

        [Fact]
        public async Task Error_User_Not_Found()
        {
            var user = UserBuilder.Build();

            var loggedUser = LoggedUserBuilder.Build(user);
            var repository = new UserWriteOnlyRepositoryBuilder().Build(); 
            var unitOfWork = UnitOfWorkBuilder.Build();

            var useCase = new DeleteUserUseCase(loggedUser, repository, unitOfWork);

            var act = async () => { await useCase.Execute(); };

            var exception = await act.ShouldThrowAsync<NotFoundException>();
            exception.GetErrors().ShouldContain(ResourceErrorMessages.USER_NOT_FOUND);
        }

        private DeleteUserUseCase CreateUseCase(User user)
        {
            var loggedUser = LoggedUserBuilder.Build(user);
            var repository = new UserWriteOnlyRepositoryBuilder()
                .GetUserById(user)  
                .Delete(user)
                .Build();
            var unitOfWork = UnitOfWorkBuilder.Build();

            return new DeleteUserUseCase(loggedUser, repository, unitOfWork);
        }
    }
}