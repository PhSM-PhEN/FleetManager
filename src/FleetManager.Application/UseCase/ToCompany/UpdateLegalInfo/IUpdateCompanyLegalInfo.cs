using FleetManager.Communication.Request.ToCompany;

namespace FleetManager.Application.UseCase.ToCompany.UpdateLegalInfo
{
    public interface IUpdateCompanyLegalInfo
    {
        Task Execute(long id, RequestCompanyUpdateLegalInfoJson request);
    }
}
