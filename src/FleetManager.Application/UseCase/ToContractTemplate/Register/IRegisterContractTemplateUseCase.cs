using FleetManager.Communication.Request.ToContractTemplate;
using FleetManager.Communication.Response.ToContractTemplate;

namespace FleetManager.Application.UseCase.ToContractTemplate.Register
{
    public interface IRegisterContractTemplateUseCase
    {
        Task<ResponseContractTemplateJson> Execute(RequestContractTemplateJson request);
    }
}
