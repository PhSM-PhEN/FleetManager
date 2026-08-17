using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToVehicle;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToVehicle.Deactivate
{
    public class DeactivateVehicleUseCase(IVehicleWriteOnlyRepository repository, IUnitOfWork unitOfWork) : IDeactivateVehicleUseCase
    {
        public async Task Execute(long id)
        {
            var vehicle = await repository.GetById(id) ?? throw new NotFoundException(ResourceErrorMessages.VEHICLE_NOT_FOUND);
            vehicle.Deactivate();

            repository.Update(vehicle);
            await unitOfWork.Commit();
        }
    }
}
