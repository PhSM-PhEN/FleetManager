using FleetManager.Communication.Response.ToAddress;

namespace FleetManager.Application.UseCase.ToAddress.GetById
{
    public interface IGetByIdAddressUseCase
    {
        Task<ResponseAddressJson> Execute(long id);
    }
}
