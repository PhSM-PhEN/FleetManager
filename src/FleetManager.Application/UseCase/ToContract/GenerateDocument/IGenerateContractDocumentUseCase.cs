using FleetManager.Communication.Response.ToContract;

namespace FleetManager.Application.UseCase.ToContract.GenerateDocument
{
    public interface IGenerateContractDocumentUseCase
    {
        Task<ResponseContractDocumentJson> Execute(long contractId);
    }
}