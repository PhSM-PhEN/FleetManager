using FleetManager.communication.Requests;
using FleetManager.communication.Responses;

namespace FleetManager.Application.UseCase.ToClient.Register
{
    public interface IRegisterClientUseCase
    {
        Task<ResponseShortClientJson> Execute(RequestClientJson request);
    }
}
