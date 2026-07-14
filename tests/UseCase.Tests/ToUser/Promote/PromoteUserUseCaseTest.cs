using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using FleetManager.Application.UseCase.ToUser.Promote;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Enum;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.ToUser.Promote
{
    public class PromoteUserUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var user = UserBuilder.Build();

            var useCase = CreateUseCase(user);
            var act = async () => { await useCase.Execute(); };

            await act.ShouldNotThrowAsync();
            user.Role.ShouldBe(Roles.ADMIN);
        }

        [Fact]
        public async Task Error_Admin_Already_Exists()
        {
            var user = UserBuilder.Build();

            var loggedUser = LoggedUserBuilder.Build(user);
            var writeRepository = new UserWriteOnlyRepositoryBuilder().Build();
            var readRepository = new UserReadOnlyRepositoryBuilder()
                .ExistsByRole(Roles.ADMIN, exists: true)
                .Build();
            var unitOfWork = UnitOfWorkBuilder.Build();

            var useCase = new PromoteUserUseCase(writeRepository, readRepository, loggedUser, unitOfWork);
            var act = async () => { await useCase.Execute(); };

            var exception = await act.ShouldThrowAsync<ErrorOnValidationException>();
            exception.GetErrors().ShouldContain(ResourceErrorMessages.ADMIN_ALREADY_EXISTS);
        }

        [Fact]
        public async Task Error_User_Not_Found()
        {
            var user = UserBuilder.Build();

            var loggedUser = LoggedUserBuilder.Build(user);
            var writeRepository = new UserWriteOnlyRepositoryBuilder().Build(); 
            var readRepository = new UserReadOnlyRepositoryBuilder()
                .ExistsByRole(Roles.ADMIN, exists: false)
                .Build();
            var unitOfWork = UnitOfWorkBuilder.Build();

            var useCase = new PromoteUserUseCase(writeRepository, readRepository, loggedUser, unitOfWork);
            var act = async () => { await useCase.Execute(); };

            var exception = await act.ShouldThrowAsync<NotFoundException>();
            exception.GetErrors().ShouldContain(ResourceErrorMessages.USER_NOT_FOUND);
        }

        private PromoteUserUseCase CreateUseCase(User user)
        {
            var loggedUser = LoggedUserBuilder.Build(user);
            var writeRepository = new UserWriteOnlyRepositoryBuilder()
                .GetUserById(user)
                .Update(user)
                .Build();
            var readRepository = new UserReadOnlyRepositoryBuilder()
                .ExistsByRole(Roles.ADMIN, exists: false)
                .Build();
            var unitOfWork = UnitOfWorkBuilder.Build();

            return new PromoteUserUseCase(writeRepository, readRepository, loggedUser, unitOfWork);
        }
    }
}