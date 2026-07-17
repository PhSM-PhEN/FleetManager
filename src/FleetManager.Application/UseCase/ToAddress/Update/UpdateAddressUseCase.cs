using FleetManager.Communication.Request.ToAddress;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToAddress;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToAddress.Update
{
    public class UpdateAddressUseCase(IAddressWriteOnlyRepository repository, IUnitOfWork unitOfWork) : IUpdateAddressUseCase
    {
        public async Task Execute(long id, RequestAddressJson request)
        {
            var address = await repository.GetById(id) 
                        ?? throw new NotFoundException(ResourceErrorMessages.ADDRESS_NOT_FOUND);
            Validate(request);
            address.Update(request.Street, request.Number, request.City, request.State, request.ZipCode);

            repository.Update(address);
            await unitOfWork.Commit();
            
        }
        private void Validate(RequestAddressJson request)
        {
            var Validator = new AddressValidator();
            var result = Validator.Validate(request);

            if(result.IsValid == false)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errors);
            }
        }
    }
}
