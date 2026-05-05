using System;
using AutoMapper;
using FleetManager.communication.Resposnes.ToAddress;
using FleetManager.Domain.Repositories.ToAddress;

namespace FleetManager.Application.UseCase.ToAddress.GetAll;

public class AddressGetAll(IAddressReadOnlyRepository repository, IMapper mapper) : IAddressGetAll
{
    private readonly IAddressReadOnlyRepository _repository = repository;
    private readonly IMapper _mapper = mapper;
    public async Task<ResponseAddressJson> Execute()
    {
        var address =  await _repository.GetAll();
        
       return new ResponseAddressJson
       {
           // falta o list response para o address
       };
        
    }
}

   
