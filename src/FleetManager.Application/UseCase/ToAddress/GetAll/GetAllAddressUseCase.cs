using FleetManager.Application.Extensions;
using FleetManager.Communication.Responses;
using FleetManager.Domain.Repositories.ToAddress;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToAddress.GetAll
{
    public class GetAllAddressUseCase(IAddressReadOnlyRepository repository) : IGetAllAddressUseCase
    {
        public async Task<List<ResponseShortAddressJson>> Execute()
        {
            var address =  await repository.GetAll();

            if (address.Count == 0)
            {
                throw new NotFoundException(ResourceErrorMessages.ADDRESS_NOT_FOUND);
            }

            return address.ToShortResponse();
        
        }
    }

   
}
