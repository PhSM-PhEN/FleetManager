using FleetManager.Application.Extensions;
using FleetManager.Communication.Request.ToUser;
using FleetManager.Communication.Response.ToUser;
using FleetManager.Domain.Repositories.ToUser;
using FleetManager.Domain.Security.CryptoGraphy;
using FleetManager.Domain.Security.Token;
using FleetManager.Exception.ExceptionBase;
using Microsoft.Extensions.Logging;

namespace FleetManager.Application.UseCase.DoLogin
{
    public class DoLoginUseCase(
        IUserReadOnlyRepository repository,
        IPasswordEncrypter passwordEncrypter,
        IAccessTokenGenerator tokenGenerator,
        ILogger<DoLoginUseCase> logger) : IDoLoginUseCase
    {
        public async Task<ResponseLoginUserJson> Execute(RequestLoginUserJson request)
        {
            var user = await repository.GetUserByEmail(request.Email);

            // Mesma resposta (mensagem, status) pra "email não existe" e "senha errada" — não dar
            // nenhuma pista pro cliente de qual das duas falhou (evita enumeração de emails).
            // O motivo real da falha só vai pro log interno, nunca na resposta HTTP.
            if (user is null)
            {
                logger.LogWarning("Login failed: no user found for email {Email}", request.Email);
                throw new InvalidLoginException();
            }

            var passwordMatch = passwordEncrypter.Verify(request.Password, user.Password);

            if (!passwordMatch)
            {
                // Nunca logar a senha (nem em texto plano, nem hasheada) — só o e-mail, que já
                // é suficiente pra detectar tentativas de força bruta/credential stuffing.
                logger.LogWarning("Login failed: invalid password for email {Email}", request.Email);
                throw new InvalidLoginException();
            }

            var token = tokenGenerator.GenerateToken(user);

            return user.ToLoginResponse(token);
        }
    }
}
