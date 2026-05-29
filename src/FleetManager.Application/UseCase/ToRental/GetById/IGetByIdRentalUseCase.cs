using System;
using FleetManager.Communication.Responses;

namespace FleetManager.Application.UseCase.ToRental.GetById;

public interface IGetByIdRentalUseCase
{
    Task<ResponseRentalInfoJson> Execute(long id);
}
