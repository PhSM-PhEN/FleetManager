using FleetManager.Application.Extensions;
using FleetManager.Communication.Requests;
using FleetManager.Communication.Responses;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToAddress;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToAddress.Register
{
    public class RequestAddressUseCase(IUnitOfWork unitOfWork, IAddressWriteOnlyRepository repository) : IRequestRegisterAddressUseCase
    {

        public async Task<ResponseAddressJson> Execute(RequestAddressJson request)
        {
            Validate(request);
            var address = new Address(request.Street, request.Number, request.City, request.State, request.ZipCode);

            await repository.Add(address);
            await unitOfWork.Commit();

            return address.ToResponse();
        }
        private static void Validate(RequestAddressJson request)
        {
            var validator = new AddressValidator();
            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errors = result.Errors.Select(erro => erro.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errors);
            }
        }
    }
}
