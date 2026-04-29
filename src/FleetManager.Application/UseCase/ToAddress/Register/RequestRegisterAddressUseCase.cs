using System;
using AutoMapper;
using FleetManager.communication.Requests.ToAddress;
using FleetManager.communication.Resposnes.ToAddress;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToAddress;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToAddress.Register;

public class RequestRegisterAddressUseCase(IMapper mapper, IUnitOfWork unitOfWork, IAddressWriteOnlyRepository repository) : IRequestRegisterAdressUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAddressWriteOnlyRepository _repository = repository;
    public async Task<ResponseAddressJson> Execute(RequestAddressJson request)
    {
        Validate(request);
        var address = _mapper.Map<Address>(request);
        await _repository.Add(address);
        await _unitOfWork.Commit();

       return _mapper.Map<ResponseAddressJson> (address);
    }
    private static void Validate(RequestAddressJson request)
    {
        var validator = new AddressValidator();
        var result =  validator.Validate(request);

        if(result.IsValid == false)
        {
            var errors = result.Errors.Select(erro => erro.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errors);
        }
    }
}
