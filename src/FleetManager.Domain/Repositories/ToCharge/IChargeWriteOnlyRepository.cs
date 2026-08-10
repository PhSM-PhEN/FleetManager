using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToCharge
{
    public interface IChargeWriteOnlyRepository
    {
        Task Add(Charge charge);
    }
}
