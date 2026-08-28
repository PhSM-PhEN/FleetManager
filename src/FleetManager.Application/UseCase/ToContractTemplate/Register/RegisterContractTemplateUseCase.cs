using FleetManager.Application.Extensions;
using FleetManager.Communication.Request.ToContractTemplate;
using FleetManager.Communication.Response.ToContractTemplate;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToContractTemplate;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToContractTemplate.Register
{
    public class RegisterContractTemplateUseCase(
        IContractTemplateWriteOnlyRepository repository,
        IUnitOfWork unitOfWork) : IRegisterContractTemplateUseCase
    {
        public async Task<ResponseContractTemplateJson> Execute(RequestContractTemplateJson request)
        {
            Validate(request);

            var template = new ContractTemplate(request.Name, request.Content, version: 2);

            await repository.Add(template);
            await unitOfWork.Commit();

            return template.ToResponse();
        }

        private static void Validate(RequestContractTemplateJson request)
        {
            var validator = new ContractTemplateValidator();
            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errors = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errors);
            }
        }
    }
}
