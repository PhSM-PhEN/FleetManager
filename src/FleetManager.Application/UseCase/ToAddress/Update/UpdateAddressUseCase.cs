using System;
using AutoMapper;
using FleetManager.communication.Requests.ToAddress;
using FleetManager.communication.Resposnes.ToAddress;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToAddress;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToAddress.Update;

public class UpdateAddressUseCase(IAddressUpdateOnlyRepository updateOnlyRepository, IMapper mapper, IUnitOfWork unitOfWork) : IUpdateAddressUseCase
{
    private readonly IAddressUpdateOnlyRepository _updateOnlyRepository = updateOnlyRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<ResponseAddressJson> Execute(long id ,RequestAddressJson request)
    {   
        Validate(request);
        var address = await _updateOnlyRepository.GetById(id);

        if(address == null)
        {
            
        }

        throw new NotImplementedException();
    }
    private void Validate(RequestAddressJson request)
    {
        var validator = new AddressValidator();
        var result = validator.Validate(request);

        if (result.IsValid == false)
        {
            var errorMessage = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessage);
        }
    }
}


