using System;
using AutoMapper;
using FleetManager.communication.Resposnes.ToAddress;
using FleetManager.Domain.Repositories.ToAddress;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToAddress.GetById;

public class GetByIdAddressUseCase(IAddressReadOnlyRepository repository, IMapper mapper) : IGetByIdAddressUseCase
{
    private readonly IAddressReadOnlyRepository _repository = repository;
    private readonly IMapper _mapper = mapper;
    public async Task<ResponseAddressJson> Execute(long id)
    {
        var address = await _repository.GetById(id);

        if(address is null)
        {
            throw new NotFoundException(ResourceErrorMessages.NOT_FOUND);
        }
        return _mapper.Map<ResponseAddressJson>(address);

        
    }
}
