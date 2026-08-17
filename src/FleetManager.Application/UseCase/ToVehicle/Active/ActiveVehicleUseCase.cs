
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToVehicle;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToVehicle.Active
{
    public class ActiveVehicleUseCase(IVehicleWriteOnlyRepository repository, IUnitOfWork unitOfWork) : IActiveVehicleUseCase
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
