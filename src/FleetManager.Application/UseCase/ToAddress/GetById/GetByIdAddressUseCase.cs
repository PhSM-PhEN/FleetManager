
using AutoMapper;
using FleetManager.communication.Responses.ToAddress;
using FleetManager.Domain.Repositories.ToAddress;

namespace FleetManager.Application.UseCase.ToAddress.GetById;

public class GetByIdAddressUseCase(IAddressReadOnlyRepository repository, IMapper mapper) : IGetByIdAddressUseCase
{
    private readonly IAddressReadOnlyRepository _repository = repository;
    private readonly IMapper _mapper = mapper;
    public async Task<ResponseAddressJson> Execute(long id)
    {
        var address = await _repository.GetById(id);

        return _mapper.Map<ResponseAddressJson>(address);
    }
}
