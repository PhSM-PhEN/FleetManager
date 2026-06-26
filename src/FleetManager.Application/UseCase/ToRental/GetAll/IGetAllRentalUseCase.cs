using FleetManager.Communication.Responses;

namespace FleetManager.Application.UseCase.ToRental.GetAll;

public interface IGetAllRentalUseCase
{
    Task<ResponsePaginatedJson<ResponseShortRentalJson>> Execute(int pageNumber, int pageSize);
}   
