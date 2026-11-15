using FleetManager.Domain.Enum;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToVehicle;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToVehicle.Delete
{
    public class DeleteVehicleUseCase(IVehicleWriteOnlyRepository repository, IUnitOfWork unitOfWork) : IDeleteVehicleUseCase
    {
        public async Task Execute(long id)
        {
            var vehicle = await repository.GetById(id) ??
                throw new NotFoundException(ResourceErrorMessages.VEHICLE_NOT_FOUND);

            if (vehicle.Status == VehicleStatus.Rented)
                throw new BusinessRuleException(ResourceErrorMessages.VEHICLE_ALREADY_RENTED);

            vehicle.SoftDelete();
            repository.Update(vehicle);
            await unitOfWork.Commit();
        }
    }
}
