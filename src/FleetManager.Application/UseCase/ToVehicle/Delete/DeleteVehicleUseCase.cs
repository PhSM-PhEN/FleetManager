using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToVehicle;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToVehicle.Delete
{
    public class DeleteVehicleUseCase(IUnitOfWork unitOfWork, IVehicleReadOnlyRepository readOnlyRepository, IVehicleWriteOnlyRepository writeOnlyRepository) : IDeleteVehicleUseCase
    {

        public async Task Execute(long id)
        {
            var vehicle = await readOnlyRepository.GetById(id);
            if (vehicle == null)
            {
                throw new NotFoundException(ResourceErrorMessages.VEHICLE_NOT_FOUND);
            }
            await writeOnlyRepository.Delete(vehicle.Id);
            await unitOfWork.Commit();
        }
    }
}
