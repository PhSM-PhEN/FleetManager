using FleetManager.Communication.Requests;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToUser;
using FleetManager.Domain.Security.Cryptography;
using FleetManager.Domain.Services.LoggedUser;
using FleetManager.Exception.ExceptionBase;
using FluentValidation.Results;

namespace FleetManager.Application.UseCase.ToUser.ChangePassword
{
    public class ChangePasswordUserUseCase(ILoggedUser loggedUser, IPasswordEncrypter passwordEncripter, IUserUpdateOnlyRepository updateRepository, IUnitOfWork unitOfWork) : IChangePasswordUserUseCase
    {

        public async Task Execute(RequestChangePasswordJson request)
        {
            var logged = await loggedUser.Get();


            var user = await updateRepository.GetById(logged.Id);

            Validate(request, user);
            user.ChangePassword(passwordEncripter.Encrypt(request.NewPassword));

            updateRepository.Update(user);
            await unitOfWork.Commit();
        }


        private void Validate(RequestChangePasswordJson request, User user)
        {
            var validator = new ChangePasswordValidator();
            var result = validator.Validate(request);

            var passwordMach = passwordEncripter.Verify(request.Password, user.Password);


            if (passwordMach == false)
            {
                result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_OR_PASSWORD_INVALID));
            }
            if (result.IsValid == false)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errors);
            }


        }
    }
}
