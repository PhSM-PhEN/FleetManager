using FleetManager.Application.Extensions;
using FleetManager.Communication.Requests;
using FleetManager.Communication.Responses;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToClient;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToClient.Register
{
    public class RegisterClientUseCase(IClientWriteOnlyRepository repository, IUnitOfWork unitOfWork) : IRegisterClientUseCase
    {

        public async Task<ResponseShortClientJson> Execute(RequestClientJson request)
        {
            Validate(request);

            var client = new Client(request.FirstAndLastName, request.PhoneNumber, request.RG,
            request.CPF, request.CnhRegisterNumber, request.CnhCategory, request.AddressId);

            await repository.Add(client);
            await unitOfWork.Commit();

            return client.ToShortResponse();

        }
        private static void Validate(RequestClientJson request)
        {
            var validator = new ClientValidator();

            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errors);
            }

        }

    }
}
