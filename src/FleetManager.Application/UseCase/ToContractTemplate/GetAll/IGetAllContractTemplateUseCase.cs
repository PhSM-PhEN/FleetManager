using FleetManager.Communication.Response;
using FleetManager.Communication.Response.ToContractTemplate;

namespace FleetManager.Application.UseCase.ToContractTemplate.GetAll
{
    public interface IGetAllContractTemplateUseCase
    {
        Task<ResponsePaginatedJson<ResponseContractTemplateJson>> Execute(int pageNumber, int pageSize, bool? onlyActive = null);
    }
}
