
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToVehicle;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToVehicle.Desactive
{
    public class DesactiveVehicleUseCase(IVehicleWriteOnlyRepository repository, IUnitOfWork unitOfWork) : IDesactiveVehicleUsCase
    {
        public async Task Execute(long id)
        {
            var vehicle = await repository.GetById(id) ?? throw new NotFoundException(ResourceErrorMessages.VEHICLE_NOT_FOUND);
            vehicle.Desactivate();

            repository.Update(vehicle);
            await unitOfWork.Commit();
        }
    }
}
