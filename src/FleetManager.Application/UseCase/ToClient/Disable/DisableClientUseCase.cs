using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToClient;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToClient.Disable
{
    public class DisableClientUseCase(IClientUpdateOnlyRepository clientRepository, IUnitOfWork unitOfWork) : IDisableClientUseCase
    {


        public async Task Execute(long id)
        {
            var client = await clientRepository.GetById(id) ??
                throw new NotFoundException(ResourceErrorMessages.CLIENT_NOT_FOUND);
            
            client.Disable();
            clientRepository.Update(client);
            await unitOfWork.Commit();
        }
    }
}
