using FleetManager.Application.Extensions;
using FleetManager.Communication.Response.ToUser;
using FleetManager.Domain.Repositories.ToUser;
using FleetManager.Domain.Services.LoggedUser;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToUser.GetProfile;

public class GetProfileUserUseCase(IUserReadOnlyRepository repository, ILoggedUser loggedUser) : IGetProfileUserUseCase
{
    public async Task<ResponseProfileUserJson> Execute()
    {
        var logged = await loggedUser.Get();
        var user = await repository.GetUserById(logged.Id) 
                    ?? throw new NotFoundException(ResourceErrorMessages.USER_NOT_FOUND);
        

        return user.ToProfileResponse();
    }
}
