
using FleetManager.Domain.Repositories.ToContract;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToContract.Delete
{
    public class DeleteContractUseCase(IContractWriteOnlyRepository repository) : IDeleteContractUseCase
    {
        public async Task Execute(long id)
        {
            var contratc = await repository.GetById(id) ?? 
                throw new NotFoundException("");
            throw new NotImplementedException();
        }
    }
}
