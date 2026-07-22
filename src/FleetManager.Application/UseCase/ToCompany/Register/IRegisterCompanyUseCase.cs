using FleetManager.Communication.Request.ToCompany;
using FleetManager.Communication.Response.ToCompany;

namespace FleetManager.Application.UseCase.ToCompany.Register
{
    public interface IRegisterCompanyUseCase
    {
        Task<ResponseShortCompanyJson> Execute(RequestCompanyJson request);
    }
}