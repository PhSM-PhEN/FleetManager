using FleetManager.Communication.Requests;
using FleetManager.Communication.Responses;

namespace FleetManager.Application.UseCase.ToClient.Register
{
    public interface IRegisterClientUseCase
    {
        Task<ResponseShortClientJson> Execute(RequestClientJson request);
    }
}
