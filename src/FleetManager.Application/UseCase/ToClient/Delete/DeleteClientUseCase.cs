using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToClient;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToClient.Delete
{
    public class DeleteClientUseCase (IClientWriteOnlyRepository repository, IClientReadOnlyRepository clientRepository, IUnitOfWork unitOfWork) : IDeleteClientUseCase
    {
        private readonly IClientWriteOnlyRepository _repository = repository;
        private readonly IClientReadOnlyRepository _clientRepository = clientRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task Execute(long id)
        {
            var client = await _clientRepository.GetById(id);

            if (client == null)
            {
                throw new NotFoundException(ResourceErrorMessages.CLIENT_NOT_FOUND);
            }

            await _repository.Delete(id);
            await _unitOfWork.Commit();
        }
    }
}
