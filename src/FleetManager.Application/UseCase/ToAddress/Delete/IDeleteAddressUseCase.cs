using System;

namespace FleetManager.Application.UseCase.ToAddress.Delete;

public interface IDeleteAddressUseCase
{
    Task Execute(long id);
}
