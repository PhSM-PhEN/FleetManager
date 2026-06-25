using FleetManager.Application.Extensions;
using FleetManager.Communication.Requests;
using FleetManager.Communication.Responses;
using FleetManager.Domain.Repositories.ToUser;
using FleetManager.Domain.Security.Cryptography;
using FleetManager.Domain.Security.Token;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToLogin
{
    public class DoLoginUseCase(IUserReadOnlyRepository userReadOnlyRepository, IPasswordEncrypter passwordEncripter,
        IAccessTokenGenerator tokenGenerator) : IDoLoginUseCase
    {



        public async Task<ResponseLoginJson> Execute(RequestLoginUserJson request)
        {
            var user = await userReadOnlyRepository.GetUserByEmail(request.Email) 
                ?? throw new InvalidLoginException(); 
            var passwordMatch = passwordEncripter.Verify(request.Password, user.Password);


            if(!passwordMatch)
            {
                throw new InvalidLoginException();
            }
            var token = tokenGenerator.GenerateToken(user);
            
            return user.ToLoginResponse(token);
        }
    }
}
