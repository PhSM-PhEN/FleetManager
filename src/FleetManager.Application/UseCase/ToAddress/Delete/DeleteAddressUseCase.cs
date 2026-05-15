using System;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToAddress;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToAddress.Delete;

public class DeleteAddressUseCase(IAddressReadOnlyRepository readRepository,IAddressWriteOnlyRepository repository, IUnitOfWork unitOfWork) : IDeleteAddressUseCase
{
    private readonly IAddressReadOnlyRepository _readRepoditory = readRepository;
    private readonly IAddressWriteOnlyRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task Execute(long id)
    {
        var address = await _readRepoditory.GetById(id);
        if (address is null )
        {
            throw new NotFoundException(ResourceErrorMessages.ADDRESS_NOT_FOUND);
        }
        await _repository.Delete(id);
        await _unitOfWork.Commit();

        
    }
}
