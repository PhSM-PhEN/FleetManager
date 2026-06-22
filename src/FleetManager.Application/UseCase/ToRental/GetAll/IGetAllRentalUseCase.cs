using FleetManager.Communication.Responses;

namespace FleetManager.Application.UseCase.ToRental.GetAll;

public interface IGetAllRentalUseCase
{
    Task<ResponsePaginatedJson<ResponseRentalJson>> Execute(int pageNumber, int pageSize);
}   
