using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToContract;
using FleetManager.Domain.Repositories.ToVehicle;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToContract.Cancel
{
    public class CancelContractUseCase(
        IContractWriteOnlyRepository contractRepository,
        IVehicleWriteOnlyRepository vehicleRepository,
        IUnitOfWork unitOfWork) : ICancelContractUseCase
    {
        public async Task Execute(long id)
        {
            var contract = await contractRepository.GetById(id) ??
                throw new NotFoundException(ResourceErrorMessages.CONTRACT_NOT_FOUND);

            var vehicle = await vehicleRepository.GetById(contract.VehicleId) ??
                throw new NotFoundException(ResourceErrorMessages.VEHICLE_NOT_FOUND);

            contract.Cancel();
            vehicle.MarkasAvailable();

            vehicleRepository.Update(vehicle);
            contractRepository.Update(contract);
            await unitOfWork.Commit();
        }
    }
}
