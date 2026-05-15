
using FleetManager.communication.Requests.ToClient;
using FleetManager.communication.Resposnes.ToClient;

namespace FleetManager.Application.UseCase.ToClient.Register;

public interface IRegisterClientUseCase
{
    Task<ResponseShortClientJson> Execute(RequestClientJson request);
}
