using FleetManager.Communication.Requests;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToCompany;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToCompany.Update;

public class UpdateCompanyUseCase(ICompanyUpdateOnlyRepository repository, IUnitOfWork unitOfWork) : IUpdateCompanyUseCase
{

    public async Task Execute(long id, RequestUpdateCompanyJson request)
    {
        Validate(request);
        var company = await repository.GetById(id);
        if(company is null)
        {
            throw new NotFoundException(ResourceErrorMessages.COMPANY_NOT_FOUND);
        }
        company.Update(request.Name, request.PhoneNumber, request.AddressId);
        repository.Update(company);
        
        await unitOfWork.Commit();
    }
    private void Validate(RequestUpdateCompanyJson request)
    {
        var validator = new CompanyUpdateValidator();
        var result = validator.Validate(request);

        if(result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
    
