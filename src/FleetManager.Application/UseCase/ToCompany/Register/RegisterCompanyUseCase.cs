using System;
using AutoMapper;
using FleetManager.Communication.Requests;
using FleetManager.Communication.Responses;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToCompany;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToCompany.Register;

public class RegisterCompanyUseCase(IMapper mapper, ICompanyWriteOnlyRepository repository, IUnitOfWork unitOfWork) : IRegisterCompanyUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly ICompanyWriteOnlyRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<ResponseCompanyJson> Execute(RequestCompanyJson request)
    {
        Validate(request);
        var company = _mapper.Map<Company>(request);
        await _repository.Add(company);
        await _unitOfWork.Commit();
        
        return _mapper.Map<ResponseCompanyJson>(company);
    }
    private void Validate (RequestCompanyJson request)
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
