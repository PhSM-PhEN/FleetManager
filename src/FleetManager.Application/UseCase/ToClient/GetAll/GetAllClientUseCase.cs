using System;
using AutoMapper;
using FleetManager.communication.Responses.ToClient;
using FleetManager.Domain.Repositories.ToClient;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToClient.GetAll;

public class GetAllClientUseCase(IMapper mapper, IClientReadOnlyRepository repository) : IGetAllClientUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly IClientReadOnlyRepository _repository = repository;
   
    async Task<ResponseListClientJson> IGetAllClientUseCase.Execute()
    {
        var client = await _repository.GetAll();

        if(client.Count == 0)
        {
            throw new NotFoundException("Client Not Found.");
        }    
        

        return  new ResponseListClientJson
        {
            Clients = _mapper.Map<List<ResponseShortClientJson>>(client) 
        };

        throw new NotImplementedException();
    }
}