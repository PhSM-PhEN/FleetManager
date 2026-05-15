using FleetManager.communication.Requests.ToUser;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToUser;
using FleetManager.Domain.Security.Cryptography;
using FleetManager.Domain.Services.LoggeUser;
using FleetManager.Exception.ExceptionBase;
using FluentValidation.Results;

namespace FleetManager.Application.UseCase.ToUser.ChangePassword
{
    public class ChangePasswordUserUseCase(ILoggedUser loggedUser, IPasswordEncripter passwordEncripter, IUserUpdateOnlyRepository updateRepository, IUnitOfWork unitOfWork) : IChangePasswordUserUseCase
    {
        private readonly ILoggedUser _loggedUser = loggedUser;
        private readonly IPasswordEncripter _passwordEncripter = passwordEncripter;
        private readonly IUserUpdateOnlyRepository _updateRepository = updateRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task Execute(RequestChangePasswordJson request)
        {
            var loggedUser = await _loggedUser.Get();
            Validate(request, loggedUser);

            var user = await _updateRepository.GetById(loggedUser.Id);
            user.Password = _passwordEncripter.Encrypt(request.NewPassword);

           _updateRepository.Update(user);
            await _unitOfWork.Commit();
        }


        private void Validate(RequestChangePasswordJson request, User user)
        {
            var validator = new ChangePasswordValidator();
            var result = validator.Validate(request);

            var passwordMach = _passwordEncripter.Verify(request.Password, user.Password);


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
