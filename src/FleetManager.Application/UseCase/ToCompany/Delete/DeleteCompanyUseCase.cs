using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToCompany;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToCompany.Delete
{
    public class DeleteCompanyUseCase(ICompanyWriteOnlyRepository repository, IUnitOfWork unitOfWork) : IDeleteCompanyUseCase
    {
        public async Task Execute(long id)
        {
            var company = await repository.GetById(id) ??
                throw new NotFoundException(ResourceErrorMessages.COMPANY_NOT_FOUND);

            await repository.Delete(company.Id);
            await unitOfWork.Commit();
        }
    }
}