using System;
using AutoMapper;
using FleetManager.Communication.Responses;
using FleetManager.Domain.Repositories.ToRental;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToRental.GetById;

public class GetByIdRentalUseCase(IMapper mapper, IRentalReadOnlyRepository repository) : IGetByIdRentalUseCase
{
    public async Task<ResponseRentalInfoJson> Execute(long id)
    {
        var rentalDetail = await repository.GetById(id);
        if(rentalDetail == null)
        {
            throw new NotFoundException("Rental not found");
        }
        return mapper.Map<ResponseRentalInfoJson>(rentalDetail);
    }
}
