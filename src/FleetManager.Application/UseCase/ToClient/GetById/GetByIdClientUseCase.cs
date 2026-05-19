using System;
using AutoMapper;
using FleetManager.communication.Responses.ToClient;
using FleetManager.Domain.Repositories.ToClient;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToClient.GetById;

public class GetByIdClientUseCase(IMapper mapper, IClientReadOnlyRepository repository) : IGetByIdClientUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly IClientReadOnlyRepository _repository = repository;
    public async Task<ResponseClientJson> Execute(long id)
    {
        var client = await _repository.GetById(id);
        if(client == null)
        {
            throw new NotFoundException("Client not found");
        }
        return _mapper.Map<ResponseClientJson>(client);
        
    }
}
