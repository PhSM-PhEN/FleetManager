using FleetManager.Communication.Requests;

namespace FleetManager.Application.UseCase.ToRental.Update;

public interface IUpdateRentalUseCase
{
    Task Execute(long id, RequestUpdateRentJson request);
}
