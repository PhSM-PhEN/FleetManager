using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToVehicle;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToVehicle.Activate
{
    public class ActivateVehicleUseCase(IVehicleWriteOnlyRepository repository, IUnitOfWork unitOfWork) : IActivateVehicleUseCase
    {
        public async Task Execute(long id)
        {
            var vehicle = await repository.GetById(id) ?? throw new NotFoundException(ResourceErrorMessages.VEHICLE_NOT_FOUND);
            vehicle.Activate();
            repository.Update(vehicle);

            await unitOfWork.Commit();
        }
    }
}
