using FleetManager.Communication.Requests;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToVehicle;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToVehicle.Update.UpdateVehicle
{
    public class UpdateVehicleUseCase(IUnitOfWork unitOfWork, IVehicleUpdateOnlyRepository repository) : IUpdateVehicleUseCase
    {

        public async Task Execute(long id, RequestUpdateVehicleJson request)
        {
            Validate(request);
            var vehicle = await repository.GetById(id);
            if (vehicle == null)
            {
                throw new NotFoundException(ResourceErrorMessages.VEHICLE_NOT_FOUND);
            }

            vehicle.Update(request.Brand, request.Model, request.Color, request.CategoryId);

             repository.Update(vehicle);

            await unitOfWork.Commit();
        }
        private static void Validate(RequestUpdateVehicleJson request)
        {
            var validator = new VehicleUpdateValidator();
            var result = validator.Validate(request);
            if (result.IsValid == false)
            {
                var errorMessage = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessage);
            }
        }
    }
}
