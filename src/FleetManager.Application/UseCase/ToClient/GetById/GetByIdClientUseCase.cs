using FleetManager.Application.Extensions;
using FleetManager.Communication.Responses;
using FleetManager.Domain.Repositories.ToClient;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToClient.GetById;

public class GetByIdClientUseCase(IClientReadOnlyRepository repository) : IGetByIdClientUseCase
{

    public async Task<ResponseClientJson> Execute(long id)
    {
        var client = await repository.GetById(id)
            ?? throw new NotFoundException(ResourceErrorMessages.CLIENT_NOT_FOUND);

        return client.ToDetailResponse();

    }
}
