using FleetManager.Application.Extensions;
using FleetManager.Communication.Response.ToContractTemplate;
using FleetManager.Domain.Repositories.ToContractTemplate;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToContractTemplate.GetById
{
    public class GetByIdContractTemplateUseCase(IContractTemplateReadOnlyRepository repository) : IGetByIdContractTemplateUseCase
    {
        public async Task<ResponseContractTemplateJson> Execute(long id)
        {
            var template = await repository.GetById(id) ??
                throw new NotFoundException(ResourceErrorMessages.CONTRACT_TEMPLATE_NOT_FOUND);

            return template.ToResponse();
        }
    }
}
