using System;
using AutoMapper;
using FleetManager.communication.Requests;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToCompany;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToCompany.Update;

public class UpdateCompanyUseCase(IMapper mapper, ICompanyUpdateOnlyRepository repository, IUnitOfWork unitOfWork) : IUpdateCompanyUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly ICompanyUpdateOnlyRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task Execute(int id, RequestCompanyJson request)
    {
        Validate(request);
        var company = await _repository.GetById(id);
        if(company is null)
        {
            throw new NotFoundException("Company not found");
        }
        _mapper.Map(request, company);
        _repository.Update(company);
        
        await _unitOfWork.Commit();
    }
    private void Validate(RequestCompanyJson request)
    {
        var validator = new CompanyValidator();
        var result = validator.Validate(request);

        if(result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
    
