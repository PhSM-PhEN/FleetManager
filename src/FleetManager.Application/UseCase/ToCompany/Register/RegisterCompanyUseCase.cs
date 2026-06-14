using AutoMapper;
using FleetManager.Communication.Requests;
using FleetManager.Communication.Responses;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToCompany;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToCompany.Register;

public class RegisterCompanyUseCase(IMapper mapper, ICompanyWriteOnlyRepository repository,
    ICompanyReadOnlyRepository readOnlyRepository,IUnitOfWork unitOfWork) : IRegisterCompanyUseCase
{

    public async Task<ResponseCompanyJson> Execute(RequestCompanyJson request)
    {
        Validate(request);
        var company =  new Company(request.Name, request.Cnpj, request.PhoneNumber, request.AddressId);
        await repository.Add(company);
        await unitOfWork.Commit();
        
        var response = await readOnlyRepository.GetById(company.Id);
        if (response == null)
        {
            throw new NotFoundException(ResourceErrorMessages.COMPANY_NOT_FOUND);
        }

        return mapper.Map<ResponseCompanyJson>(response);
    }
    private static void Validate (RequestCompanyJson request)
    {
        var Validator = new CompanyValidator();
        var result = Validator.Validate(request);

        if(result.IsValid == false)
        {
            var errorMessage = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessage);
        }
    }
}
