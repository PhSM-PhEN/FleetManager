using FleetManager.communication.Requests.ToLogin;
using FleetManager.communication.Resposnes.ToUsers;
using FleetManager.Domain.Repositories.ToUser;
using FleetManager.Domain.Security.Cryptography;
using FleetManager.Domain.Security.Token;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToLogin
{
    public class DoLoginUseCase(IUserReadOnlyRepository userReadOnlyRepository, IPasswordEncripter passwordEncripter,
        IAccesTokenGenerator tokenGenerator) : IDoLoginUseCase
    {
        private readonly IUserReadOnlyRepository _userReadOnlyRepository = userReadOnlyRepository;
        private readonly IPasswordEncripter _passwordEncripter = passwordEncripter;
        private readonly IAccesTokenGenerator _tokenGenerator = tokenGenerator;


        public async Task<ResponseRegisterUserJson> Execute(RequestLoginUserJson request)
        {
            var user = await _userReadOnlyRepository.GetUserByEmail(request.Email) ?? throw new InvalidLoginException(); 
            var passwordMatch = _passwordEncripter.Verify(request.Password, user.Password);


            if(!passwordMatch)
            {
                throw new InvalidLoginException();
            }
            return new ResponseRegisterUserJson
            {
                Name = user.Name,
                Token = _tokenGenerator.GenerateToken(user)
            };
        }
    }
}
