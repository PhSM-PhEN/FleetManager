using FleetManager.Communication.Request.ToCompany;

namespace FleetManager.Application.UseCase.ToCompany.UpdateToAddress
{
    public interface IUpdateCompanyAddressUseCase
    {
        Task Execute(long id, RequestCompanyUpdateAddressJson request);
    }
}
