using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToVehicle;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToVehicle.Delete
{
    public class DeleteVehicleUseCase(IUnitOfWork unitOfWork, IVehicleReadOnlyRepository readOnlyRepository, IVehicleWriteOnlyRepository writeOnlyRepository) : IDeleteVehicleUseCase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IVehicleReadOnlyRepository _readOnlyRepository = readOnlyRepository;
        private readonly IVehicleWriteOnlyRepository _writeOnlyRepository = writeOnlyRepository;
        public async Task Execute(long id)
        {
            var vehicle = await _readOnlyRepository.GetById(id);
            if (vehicle == null)
            {
                throw new NotFoundException(ResourceErrorMessages.VEHICLE_NOT_FOUND);
            }
            await _writeOnlyRepository.Delete(id);
            await _unitOfWork.Commit();
        }
    }
}
