using FleetManager.Application.Extensions;
using FleetManager.Communication.Response.ToAddress;
using FleetManager.Domain.Repositories.ToAddress;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToAddress.GetById
{
    public class GetByIdAddressUseCase(IAddressReadOnlyRepository repository) : IGetByIdAddressUseCase
    {
        public async Task<ResponseAddressJson> Execute(long id)
        {
            var address = await repository.GetById(id) ??
                throw new NotFoundException(ResourceErrorMessages.ADDRESS_NOT_FOUND);
            
            return address.ToResponse();
        }
    }
}
