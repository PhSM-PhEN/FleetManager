using FleetManager.Application.Extensions;
using FleetManager.Communication.Response;
using FleetManager.Communication.Response.ToContractTemplate;
using FleetManager.Domain.Repositories.ToContractTemplate;

namespace FleetManager.Application.UseCase.ToContractTemplate.GetAll
{
    public class GetAllContractTemplateUseCase(IContractTemplateReadOnlyRepository repository) : IGetAllContractTemplateUseCase
    {
        public async Task<ResponsePaginatedJson<ResponseContractTemplateJson>> Execute(int pageNumber, int pageSize, bool? onlyActive = null)
        {
            if (pageNumber <= 0)
                pageNumber = 1;
            if (pageSize <= 0 || pageSize > 50)
                pageSize = 10;

            var (templates, totalCount) = await repository.GetAll(pageNumber, pageSize, onlyActive);

            return new ResponsePaginatedJson<ResponseContractTemplateJson>
            {
                Data = templates.ToResponse(),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
    }
}
