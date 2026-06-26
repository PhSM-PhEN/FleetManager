using FleetManager.Communication.Requests;
using FleetManager.Communication.Responses;

namespace FleetManager.Application.UseCase.ToAddress.Register
{
    public interface IRequestRegisterAddressUseCase
    {
        Task<ResponseAddressJson> Execute(RequestAddressJson request);
    }
}
