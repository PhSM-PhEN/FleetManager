using FleetManager.Communication.Request.ToUser;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToUser;
using FleetManager.Domain.Services.LoggedUser;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToUser.Update
{
    public class UpdateProfileUserUseCase(IUserWriteOnlyRepository repository,
    IUserReadOnlyRepository readRepository ,ILoggedUser logged, IUnitOfWork unitOfWork) : IUpdateProfileUserUseCase
    {
        public async Task Execute(RequestUpdateUserJson request)
        {
            var loggedUser = await logged.Get();

            var user = await repository.GetUserById(loggedUser.Id)
                        ?? throw new NotFoundException(ResourceErrorMessages.USER_NOT_FOUND);
            await Validate(request, user.Email);

            user.Update(request.Name, request.Email);

            repository.Update(user);
            await unitOfWork.Commit();
        }
        private async Task Validate(RequestUpdateUserJson request, string currentEmail)
        {
            var validator = new UpdateValidator();
            var result = validator.Validate(request);
            if(currentEmail.Equals(request.Email) == false)
            {
                var emailAlreadyExists = await readRepository.ExistByEmail(request.Email);
                if (emailAlreadyExists)
                        result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_ALREADY_REGISTERED));
            }     

            if(result.IsValid == false)
            {
                var errorMessage = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessage);
            }
        }
    }
    
}
