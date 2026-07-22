using FleetManager.Application.Extensions;
using FleetManager.Communication.Response.ToCompany;
using FleetManager.Domain.Repositories.ToCompany;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToCompany.GetById
{
    public class GetByIdCompanyUseCase(ICompanyReadOnlyRepository repository) : IGetByIdCompanyUseCase
    {
        public async Task<ResponseCompanyJson> Execute(long id)
        {
            var company = await repository.GetById(id) ??
                throw new NotFoundException(ResourceErrorMessages.COMPANY_NOT_FOUND);

            return company.ToResponse();
        }
    }
}