using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToClient;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToClient.Delete
{
    public class DeleteClientUseCase (IClientUpdateOnlyRepository clientRepository, IUnitOfWork unitOfWork) : IDeleteClientUseCase
    {

   
        public async Task Execute(long id)
        {
            var client = await clientRepository.GetById(id);

            if (client == null)
            {
                throw new NotFoundException(ResourceErrorMessages.CLIENT_NOT_FOUND);
            }
            client.Disable();
            clientRepository.Update(client);
            await unitOfWork.Commit();
        }
    }
}
