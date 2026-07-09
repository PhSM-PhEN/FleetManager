using FleetManager.Application.Extensions;
using FleetManager.Communication.Request.ToUser;
using FleetManager.Communication.Response.ToUser;
using FleetManager.Domain.Repositories.ToUser;
using FleetManager.Domain.Security.CryptoGraphy;
using FleetManager.Domain.Security.Token;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.DoLogin
{
    public class DoLoginUseCase(IUserReadOnlyRepository repository, IPasswordEncrypter passwordEncrypter, IAccessTokenGenerator tokenGenerator) : IDoLoginUseCase
    {
        public async Task<ResponseLoginUserJson> Execute(RequestLoginUserJson request)
        {
            var user = await repository.GetUserByEmail(request.Email)
                ?? throw new InvalidLoginException();

            var passwordMacth = passwordEncrypter.Verify(request.Password, user.Password);

            if (!passwordMacth)
            {
                throw new InvalidLoginException();
            }
            var token = tokenGenerator.GenerateToken(user);

            return user.ToLoginResponse(token);
        }
    }
}
