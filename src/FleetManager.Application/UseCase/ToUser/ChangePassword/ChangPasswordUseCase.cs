using FleetManager.Communication.Request.ToUser;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToUser;
using FleetManager.Domain.Security.CryptoGraphy;
using FleetManager.Domain.Services.LoggedUser;
using FleetManager.Exception.ExceptionBase;
using FluentValidation.Results;

namespace FleetManager.Application.UseCase.ToUser.ChangePassword
{
    public class ChangPasswordUseCase(ILoggedUser logged, IPasswordEncrypter encrypter,
        IUserWriteOnlyRepository repository, IUnitOfWork unitOfWork) : IChangePasswordUseCase
    {
        public async Task Execute(RequestChangPasswordJson request)
        {
            var loggedUser = await logged.Get();
            var user = await repository.GetUserById(loggedUser.Id);
            Validate(request, user);

            user.ChangePassword(encrypter.Encrypt(request.NewPassword));
            repository.Update(user);

            await unitOfWork.Commit();


        }

        private void Validate(RequestChangPasswordJson request, User user)
        {
            var validator = new ChangPassworValidator();

            var result = validator.Validate(request);

            var passwordMach = encrypter.Verify(request.OldPassword, user.Password);

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
