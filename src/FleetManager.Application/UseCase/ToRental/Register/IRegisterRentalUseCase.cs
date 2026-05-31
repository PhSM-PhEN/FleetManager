using FleetManager.Communication.Requests;
using FleetManager.Communication.Responses;

namespace FleetManager.Application.UseCase.ToRental.Register
{
    public interface IRegisterRentalUseCase
    {
        Task<ResponseRentalJson> Execute(RequestRentJson request);
    }
}
