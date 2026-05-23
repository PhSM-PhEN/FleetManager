using System;
using AutoMapper;
using FleetManager.communication.Responses;
using FleetManager.Domain.Repositories.ToCompany;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToCompany.GetAll;

public class GetAllCompanyUseCase(IMapper mapper, ICompanyReadOnlyRepository repository) : IGetAllCompanyUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly ICompanyReadOnlyRepository _repository = repository;
    public async Task<ResponseListCompanyJson> Execute()
    {
        var company = await _repository.GetAll();
        if(company.Count == 0)
        {
            throw new NotFoundException("Company not register");
        }
        return new ResponseListCompanyJson
        {
            Companies =  _mapper.Map<List<ResponseCompanyJson>>(company)
        };
    }
}
