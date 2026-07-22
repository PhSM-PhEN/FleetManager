using FleetManager.Communication.Response.ToCompany;

namespace FleetManager.Application.UseCase.ToCompany.GetAll
{
    public interface IGetAllCompanyUseCase
    {
        Task<List<ResponseShortCompanyJson>> Execute();
    }
}