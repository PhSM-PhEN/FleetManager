using FleetManager.Communication.Request.ToAddress;
using FleetManager.Communication.Response.ToAddress;

namespace FleetManager.Application.UseCase.ToAddress.Register
{
    public interface IRegisterAddressUseCase
    {
        Task<ResponseShortAddressJson> Execute(RequestAddressJson request);
    }
}
