using AutoMapper;
using FleetManager.communication.Responses.ToAddress;
using FleetManager.Domain.Repositories.ToAddress;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToAddress.GetAll;

public class GetAllAddressUseCase(IAddressReadOnlyRepository repository, IMapper mapper) : IGetAllAddressUseCase
{
    private readonly IAddressReadOnlyRepository _repository = repository;
    private readonly IMapper _mapper = mapper;
    public async Task<ResponseListAddressJson> Execute()
    {
        var address =  await _repository.GetAll();
        if (address is null)
        {
            throw new NotFoundException(ResourceErrorMessages.ADDRESS_NOT_FOUND);
        }
        
       return new ResponseListAddressJson
       {
           Address = _mapper.Map<List<ResponseShortAddressJson>>(address)
       };
        
    }
}

   
