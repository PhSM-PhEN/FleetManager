using AutoMapper;
using FleetManager.Communication.Requests;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToClient;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToClient.Update
{
    public class UpdateClientUseCase(IClientUpdateOnlyRepository updateRepository,  IUnitOfWork unitOfWork, IMapper mapper) : IUpdateClientUseCase
    {
       
        public async Task Execute(long id, RequestClientJson request)
        {
            Validate(request);
            var client = await updateRepository.GetById(id);
            if (client == null)
            {
                throw new NotFoundException(ResourceErrorMessages.CLIENT_NOT_FOUND);
            }
            mapper.Map(request, client);

            updateRepository.Update(client);
            await unitOfWork.Commit();


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
