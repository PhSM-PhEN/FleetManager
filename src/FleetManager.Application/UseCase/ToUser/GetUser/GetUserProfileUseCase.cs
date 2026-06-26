using FleetManager.Application.Extensions;
using FleetManager.Communication.Responses;
using FleetManager.Domain.Repositories.ToUser;
using FleetManager.Domain.Services.LoggedUser;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToUser.GetUser
{
    public class GetUserProfileUseCase(ILoggedUser loggedUser, IUserReadOnlyRepository userRepository) : IGetUserProfileUseCase
    {
        public async Task<ResponseUserProfileJson> Execute()
        {
            var logged = await loggedUser.Get();
            var user = await userRepository.GetUserById(logged.Id)
                ?? throw new NotFoundException(ResourceErrorMessages.NOT_FOUND);
                
            return user.ToResponse();
        }
    }
}
