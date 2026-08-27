using FleetManager.Communication.Response.ToContractTemplate;

namespace FleetManager.Application.UseCase.ToContractTemplate.GetById
{
    public interface IGetByIdContractTemplateUseCase
    {
        Task<ResponseContractTemplateJson> Execute(long id);
    }
}
