using FleetManager.Communication.Request.ToContractTemplate;

namespace FleetManager.Application.UseCase.ToContractTemplate.Update
{
    public interface IUpdateContractTemplateUseCase
    {
        Task Execute(long id, RequestContractTemplateJson request);
    }
}