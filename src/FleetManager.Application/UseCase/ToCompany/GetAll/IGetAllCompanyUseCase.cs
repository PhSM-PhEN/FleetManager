
using FleetManager.communication.Responses;

namespace FleetManager.Application.UseCase.ToCompany.GetAll;

public interface IGetAllCompanyUseCase
{
    Task<ResponseListCompanyJson> Execute();
}
