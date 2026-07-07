using FleetManager.Application.Extensions;
using FleetManager.Communication.Request.ToUser;
using FleetManager.Communication.Response.ToUser;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToUser;
using FleetManager.Domain.Security.CryptoGraphy;
using FleetManager.Domain.Security.Token;
using FleetManager.Exception.ExceptionBase;
using FluentValidation.Results;

namespace FleetManager.Application.UseCase.ToUser.Register
{
    public class RegisterUserUseCase(IUnitOfWork unitOfWork, IUserReadOnlyRepository readOnlyRepository, IUserWriteOnlyRepository repository, IPasswordEncrypter passwordEncrypter, IAccessTokenGenerator tokenGenerator) : IRegisterUserUseCase
    {
        public async Task<ResponseLoginUserJson> Execute(RequestRegisterUserJson request)
        {
            await Validate(request);
            
            var anyUserExist = await readOnlyRepository.ExistByEmail(request.Email);

            var user = new User(request.Name, request.Email, passwordEncrypter.Encrypt(request.Password));

            if (!anyUserExist)
            {
                user.PromoteToAdmin();
            }

            await repository.Add(user);
            await unitOfWork.Commit();

            var token = tokenGenerator.GenerateToken(user);

            return user.ToLoginResponse(token);
        }
        private  async Task Validate(RequestRegisterUserJson request)
        {
            var validator = new UserValidator();
            var result = await validator.ValidateAsync(request);

            var emailExist = await readOnlyRepository.ExistByEmail(request.Email);

            if (emailExist)
            {
                result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_ALREADY_REGISTERED));
            }

            if (result.IsValid == false)
            {
                var errorMessage = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessage);
            }
        }
    }
}
