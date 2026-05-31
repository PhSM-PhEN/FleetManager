using AutoMapper;
using FleetManager.Communication.Responses;
using FleetManager.Domain.Services.LoggedUser;

namespace FleetManager.Application.UseCase.ToUser.GetUser
{
    public class GetUserProfileUseCase(ILoggedUser loggedUser, IMapper mapper) : IGetUserProfileUseCase
    {
        private readonly ILoggedUser _loggedUser = loggedUser;
        
        private readonly IMapper _mapper = mapper;
        public async Task<ResponseUserProfileJson> Execute()
        {
            
            var user = await _loggedUser.Get();

            return _mapper.Map<ResponseUserProfileJson>(user);
        }
    }
}
