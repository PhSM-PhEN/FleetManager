using FleetManager.communication.Requests;
using FleetManager.communication.Responses;

namespace FleetManager.Application.UseCase.ToRental.Register
{
    public interface IRegisterRentalUseCase
    {
        Task<ResponseRentalJson> Execute(RequestRentJson request);
    }
}
