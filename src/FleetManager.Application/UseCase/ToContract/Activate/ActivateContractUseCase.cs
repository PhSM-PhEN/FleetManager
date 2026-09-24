using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToCharge;
using FleetManager.Domain.Repositories.ToContract;
using FleetManager.Domain.Repositories.ToVehicle;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToContract.Activate
{
    public class ActivateContractUseCase(
        IContractWriteOnlyRepository contractRepository,
        IChargeWriteOnlyRepository chargeWriteOnly,
        IVehicleReadOnlyRepository vehicleReadOnlyRepository,
        IUnitOfWork unitOfWork) : IActivateContractUseCase
    {
        public async Task Execute(long id)
        {
            var contract = await contractRepository.GetById(id) ??
                throw new NotFoundException(ResourceErrorMessages.CONTRACT_NOT_FOUND);
            var vehicle = await vehicleReadOnlyRepository.GetById(contract.VehicleId) ??
                throw new NotFoundException(ResourceErrorMessages.VEHICLE_NOT_FOUND);


            contract.Confirm();

            var charge = Charge.ForContractStart(contract);
            await  chargeWriteOnly.Add(charge);
            contractRepository.Update(contract);
            await unitOfWork.Commit();
        }
    }
}
