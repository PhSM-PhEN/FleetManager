using FleetManager.Communication.Responses;

namespace FleetManager.Application.UseCase.ToAddress.GetAll
{
    public interface IGetAllAddressUseCase
    {
        Task<List<ResponseShortAddressJson>> Execute ();
    }
}
