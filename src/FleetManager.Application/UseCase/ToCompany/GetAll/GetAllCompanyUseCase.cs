using FleetManager.Application.Extensions;
using FleetManager.Communication.Responses;
using FleetManager.Domain.Repositories.ToCompany;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToCompany.GetAll
{
    public class GetAllCompanyUseCase(ICompanyReadOnlyRepository repository) : IGetAllCompanyUseCase
    {

        public async Task<List<ResponseCompanyJson>> Execute()
        {
            var company = await repository.GetAll()
                ?? throw new NotFoundException(ResourceErrorMessages.COMPANY_NOT_FOUND);

            return company.Toresponse();
        }
    }
}
