using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToCompany;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToCompany.Delete
{
    public class DeleteCompanyUseCase(IUnitOfWork unitOfWork, ICompanyWriteOnlyRepository repository, ICompanyReadOnlyRepository readRepository) : IDeleteCompanyUseCase
    {

        public async Task Execute(long id)
        {
            var company = await readRepository.GetById(id)
                ?? throw new NotFoundException(ResourceErrorMessages.COMPANY_NOT_FOUND);

            await repository.Delete(company.Id);
            await unitOfWork.Commit();
        }
    }
}
