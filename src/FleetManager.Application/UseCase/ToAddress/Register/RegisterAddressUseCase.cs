using FleetManager.Application.Extensions;
using FleetManager.Communication.Request.ToAddress;
using FleetManager.Communication.Response.ToAddress;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToAddress;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToAddress.Register
{
    public class RegisterAddressUseCase(IAddressWriteOnlyRepository repository, IUnitOfWork unitOfWork) : IRegisterAddressUseCase
    {
        public async Task<ResponseShortAddressJson> Execute(RequestAddressJson request)
        {
            Validate(request);

            var address = new Address(request.Street, request.Number, request.City, request.State, request.ZipCode);
            await repository.Add(address);
            await unitOfWork.Commit();

            return address.ToShortResponse();
        }
        private static void Validate(RequestAddressJson request)
        {
            var validator = new AddressValidator();
            
            var result = validator.Validate(request);
            
            if (result.IsValid == false)
            {
                var error = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(error);
            }

        }
    }
}
