using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToVehicle;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToVehicle.Delete
{
    public class DeleteVehicleUseCase(IVehicleWriteOnlyRepository repository, IUnitOfWork unitOfWork) : IDeleteVehicleUseCase
    {
        public async Task Delete(long id)
        {
            var vehicle = await repository.GetById(id) ??
                throw new NotFoundException("VEHICLE_NOT_FOUND");
            
            await repository.Delete(vehicle.Id);
            await unitOfWork.Commit();
        }
    }
}
