
using FleetManager.communication.Responses.ToCompany;

namespace FleetManager.Application.UseCase.ToCompany.GetAll;

public interface IGetAllCompanyUseCase
{
    Task<ResponseListCompanyJson> Execute();
}
