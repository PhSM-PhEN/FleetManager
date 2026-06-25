using FleetManager.Application.Extensions;
using FleetManager.Communication.Requests;
using FleetManager.Communication.Responses;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToUser;
using FleetManager.Domain.Security.Cryptography;
using FleetManager.Domain.Security.Token;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToUser.Register
{
    public class RegisterUserUseCase(IUnitOfWork unitOfWork, IUserWriteOnlyRepository repository,
        IUserReadOnlyRepository userReadOnly,
        IPasswordEncrypter encripter, IAccessTokenGenerator tokenGenerator) : IRegisterUserUseCase
    {

        public async Task<ResponseLoginJson> Execute(RequestRegisterUserJson request)
        {
            await Validate(request);

            var anyUserExist = await userReadOnly.AnyUserExist();

            var user = new User(request.Name, request.Email, encripter.Encrypt(request.Password));

            if(!anyUserExist)
            {
                user.PromoteToAdmin();
            }   

            await repository.Add(user);
            await unitOfWork.Commit();

            var token = tokenGenerator.GenerateToken(user);

            return user.ToLoginResponse(token);
           
        }
        private async Task Validate(RequestRegisterUserJson request)
        {
            var validate = new UserValidator();
            var result = validate.Validate(request);

            var emailExists = await userReadOnly.ExistByEmail(request.Email);

            if (emailExists)
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_ALREADY_REGISTERED));
            }

            if (result.IsValid == false)
            {
                var errorMessage = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessage);
            }

        }

       
    }
}
