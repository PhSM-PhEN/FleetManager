using FleetManager.Communication.Response.ToCompany;

namespace FleetManager.Application.UseCase.ToCompany.GetById
{
    public interface IGetByIdCompanyUseCase
    {
        Task<ResponseCompanyJson> Execute(long id);
    }
}