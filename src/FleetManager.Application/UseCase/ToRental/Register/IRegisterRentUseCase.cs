using FleetManager.communication.Requests.ToRental;
using FleetManager.communication.Responses.ToRental;

namespace FleetManager.Application.UseCase.ToRental.Register
{
    public interface IRegisterRentUseCase
    {
        Task<ResponseRentalJson> Execute(RequestRentJson request);
    }
}
