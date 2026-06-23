using System;
using AutoMapper;
using FleetManager.Communication.Responses;
using FleetManager.Domain.Repositories.ToCompany;

namespace FleetManager.Application.UseCase.ToCompany.GetById;

public class GetByIdCompanyUseCase(IMapper mapper, ICompanyReadOnlyRepository repository) : IGetByIdCompanyUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly ICompanyReadOnlyRepository _repository = repository;
    public async Task<ResponseCompanyJson> Execute(long id)
    {
        var company = await _repository.GetById(id);

        return  _mapper.Map<ResponseCompanyJson>(company);
    }
}
