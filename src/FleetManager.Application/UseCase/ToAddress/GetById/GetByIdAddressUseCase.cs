using FleetManager.Communication.Responses;
using FleetManager.Domain.Repositories.ToAddress;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToAddress.GetById;

public class GetByIdAddressUseCase(IAddressReadOnlyRepository repository) : IGetByIdAddressUseCase
{
    
    public async Task<ResponseAddressJson> Execute(long id)
    {
        var address = await repository.GetById(id);
        if (address == null)
        {
            throw new NotFoundException(ResourceErrorMessages.ADDRESS_NOT_FOUND);
        }

        return new ResponseAddressJson()
        {
            Id = address.Id,
            Street = address.Street,
            Number = address.Number,
            City = address.City,
            State = address.State,
            ZipCode = address.ZipCode
        };
          
    }
}
