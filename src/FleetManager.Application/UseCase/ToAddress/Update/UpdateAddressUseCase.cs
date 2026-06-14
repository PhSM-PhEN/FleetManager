using FleetManager.Communication.Requests;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToAddress;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToAddress.Update;

public class UpdateAddressUseCase(IAddressUpdateOnlyRepository updateOnlyRepository, IUnitOfWork unitOfWork) : IUpdateAddressUseCase
{
 
    public async Task Execute(long id ,RequestAddressJson request)
    {   
        Validate(request);
        var address = await updateOnlyRepository.GetById(id);

        if(address is null)
        {
            throw new NotFoundException(ResourceErrorMessages.ADDRESS_NOT_FOUND);
        }
        address.Update(request.Street, request.Number, request.City, request.State, request.ZipCode);
        
        updateOnlyRepository.Update(address);
        await unitOfWork.Commit();
        
        
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


