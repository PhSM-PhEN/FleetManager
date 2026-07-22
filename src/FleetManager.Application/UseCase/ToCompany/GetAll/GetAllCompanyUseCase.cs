using FleetManager.Application.Extensions;
using FleetManager.Communication.Response.ToCompany;
using FleetManager.Domain.Repositories.ToCompany;

namespace FleetManager.Application.UseCase.ToCompany.GetAll
{
    public class GetAllCompanyUseCase(ICompanyReadOnlyRepository repository) : IGetAllCompanyUseCase
    {
        public async Task<List<ResponseShortCompanyJson>> Execute()
        {
            var companies = await repository.GetAll();

            return companies.ToShortResponse();
        }
    }
}