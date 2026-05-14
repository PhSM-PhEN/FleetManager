using System;
using AutoMapper;
using FleetManager.communication.Requests.ToClient;
using FleetManager.communication.Resposnes.ToClient;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToClient;

namespace FleetManager.Application.UseCase.ToClient.Register;

public class RegisterClientUseCase(IClientWriteOnlyRepository repository, IMapper mapper, IUnitOfWork unitOfWork) : IRegisterClientUseCase
{
    private readonly IClientWriteOnlyRepository _respoitory = repository;
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public Task<ResponseShortClientJson> Execute(RequestClientJson request)
    {
        throw new NotImplementedException();
    }
    private void Validate(RequestClientJson request)
    {
        
    }    
    

}
