using System;
using FleetManager.Communication.Responses;

namespace FleetManager.Application.UseCase.ToRental.GetAll;

public interface IGetAllRentalUseCase
{
    Task<ResponseListRentalJson> Execute();
}   
