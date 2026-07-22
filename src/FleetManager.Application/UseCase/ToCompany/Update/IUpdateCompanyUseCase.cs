using FleetManager.Communication.Request.ToCompany;

namespace FleetManager.Application.UseCase.ToCompany.Update
{
    public interface IUpdateCompanyUseCase
    {
        Task Execute(long id, RequestCompanyJson request);
    }
}