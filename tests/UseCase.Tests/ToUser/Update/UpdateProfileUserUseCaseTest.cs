using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Request.ToUser;
using FleetManager.Application.UseCase.ToUser.Update;
using FleetManager.Domain.Entities;
using FleetManager.Exception.ExceptionBase;
using Shouldly;

namespace UseCase.Tests.ToUser.Update
{
    public class UpdateProfileUserUseCaseTest
    {
        [Fact]
        public async Task Sucess()
        {
            var user = UserBuilder.Build();
            var request = RequestUpdateUserJsonBuilder.Build();

            var useCase =  CreateUseCase(user);
            var act = async () =>  await useCase.Execute(request);
            
            await act.ShouldNotThrowAsync();
            user.Name.ShouldBe(request.Name);
            user.Email.ShouldBe(request.Email);

        }
        [Fact]
        public async Task Error_Name_Empty()
        {
            var user = UserBuilder.Build();
            var request = RequestUpdateUserJsonBuilder.Build();
            request.Name = string.Empty;

            var useCase = CreateUseCase(user);
            var act = async () => await  useCase.Execute(request);

            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();

            result.GetErrors().Count.ShouldBe(1);
            result.GetErrors().First().ToString().ShouldBe(ResourceErrorMessages.NAME_IS_REQUIRED);           
        }
        [Fact]
        public async Task Error_Email_Already_Registered()
        {
            var user = UserBuilder.Build();
            var request = RequestUpdateUserJsonBuilder.Build();
            

            var useCase = CreateUseCase(user, request.Email);
            var act = async () => await  useCase.Execute(request);

            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();

            result.GetErrors().Count.ShouldBe(1);
            result.GetErrors().First().ToString().ShouldBe(ResourceErrorMessages.EMAIL_ALREADY_REGISTERED);     
        }
        private UpdateProfileUserUseCase CreateUseCase(User user, string? email = null)
        {
            var unitOfWork = UnitOfWorkBuilder.Build();
            var logged = LoggedUserBuilder.Build(user);

            var readBuilder = new UserReadOnlyRepositoryBuilder();
            if (string.IsNullOrEmpty(email) == false)
            {
            readBuilder.ExistByEmail(email);
            }
            var read = readBuilder.Build();

            var write = new UserWriteOnlyRepositoryBuilder()
                .GetUserById(user)
                .Update(user)
                .Build();

            return new UpdateProfileUserUseCase(write, read, logged, unitOfWork);
        }
    }
}
