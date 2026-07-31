using FleetManager.Application.Extensions;
using FleetManager.Communication.Response.ToContract;
using FleetManager.Domain.Repositories.ToContract;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToContract.GetById
{
    public class GetByIdContractUseCase(IContractReadOnlyRepository repository) : IGetByIdContractUseCase
    {
        public async Task<ResponseContractJson> Execute(long id)
        {
            var contract = await repository.GetById(id) ??
                throw new NotFoundException(ResourceErrorMessages.CONTRACT_NOT_FOUND);
                
            return contract.ToInfoResponse();
        }
    }
}
