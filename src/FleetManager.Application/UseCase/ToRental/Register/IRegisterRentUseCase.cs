using FleetManager.communication.Requests;
using FleetManager.communication.Responses;

namespace FleetManager.Application.UseCase.ToRental.Register
{
    public interface IRegisterRentUseCase
    {
        Task<ResponseRentalJson> Execute(RequestRentJson request);
    }
}
