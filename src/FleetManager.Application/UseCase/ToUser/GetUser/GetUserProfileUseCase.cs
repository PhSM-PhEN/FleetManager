using AutoMapper;
using FleetManager.Communication.Responses;
using FleetManager.Domain.Repositories.ToUser;
using FleetManager.Domain.Services.LoggedUser;

namespace FleetManager.Application.UseCase.ToUser.GetUser
{
    public class GetUserProfileUseCase(ILoggedUser loggedUser, IUserReadOnlyRepository userRepository,
                                   IMapper mapper) : IGetUserProfileUseCase
    {
        public async Task<ResponseUserProfileJson> Execute()
        {
            var logged = await loggedUser.Get();
            var user = await userRepository.GetUserById(logged.Id);

            return mapper.Map<ResponseUserProfileJson>(user);
        }
    }
}
