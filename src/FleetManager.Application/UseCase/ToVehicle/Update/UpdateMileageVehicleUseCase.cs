using FleetManager.Communication.Request.ToVehicle;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToVehicle;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToVehicle.Update
{
    public class UpdateMileageVehicleUseCase(IVehicleWriteOnlyRepository repository, IUnitOfWork unitOfWork) : IUpdateMileageVehicleUseCase
    {
        public async Task Execute(long id, RequestMileageVehicleJson request)
        {
            var vehicle = await repository.GetById(id) ??
                          throw new NotFoundException(ResourceErrorMessages.VEHICLE_NOT_FOUND);
            vehicle.UpdateMileage(request.MileageVehicle);
            repository.Update(vehicle);
            await unitOfWork.Commit();

        }
        private void Validate(RequestMileageVehicleJson request)
        {
            var validator = new CurrentMiliageValidator();
            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var error = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(error);
            }
        }
    }
}
