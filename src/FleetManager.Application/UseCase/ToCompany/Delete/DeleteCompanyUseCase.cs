using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToCompany;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToCompany.Delete;

public class DeleteCompanyUseCase(IUnitOfWork unitOfWork, ICompanyWriteOnlyRepository repository, ICompanyReadOnlyRepository readRepository) : IDeleteCompanyUseCase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICompanyWriteOnlyRepository _repository = repository;
    private readonly ICompanyReadOnlyRepository _readRepository = readRepository;
    public async Task Execute(long id)
    {
        var company = await _readRepository.GetById(id);
        if(company is null)
        {
            throw new NotFoundException("Company not found");
        }
        await _repository.Delete(company.Id);
        await _unitOfWork.Commit();
    }
}
