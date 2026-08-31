using FleetManager.Communication.Request.ToCompany;

namespace FleetManager.Application.UseCase.ToCompany.UpdateTaxRegime
{
    public interface IUpdateTaxeTegimeUseCase
    {
        Task Execute(long id, RequestCompanyUpdateTaxRegimeJson request);
    }
}
