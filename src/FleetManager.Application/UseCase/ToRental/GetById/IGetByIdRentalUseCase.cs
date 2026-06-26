using FleetManager.Communication.Responses;

namespace FleetManager.Application.UseCase.ToRental.GetById
{
    public interface IGetByIdRentalUseCase
    {
        Task<ResponseRentalJson> Execute(long id);
    }
}
