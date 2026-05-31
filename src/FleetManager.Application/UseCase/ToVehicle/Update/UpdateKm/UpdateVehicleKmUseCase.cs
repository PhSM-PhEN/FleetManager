using AutoMapper;
using FleetManager.Communication.Requests;
using FleetManager.Communication.Responses;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToVehicle;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToVehicle.Update.UpdateKm
{
    public class UpdateVehicleKmUseCase(IUnitOfWork unitOfWork, IVehicleUpdateOnlyRepository repository, IMapper mapper) : IUpdateVehicleKmUseCase
    {
        public async Task<ResponseVehicleJson> Execute(long id, RequestVehicleUpdateCurrentMileageJson request)
        {
            Validate(request);
            var vehicle = await repository.GetById(id);

            if (vehicle == null)
            {
                throw new NotFoundException(ResourceErrorMessages.VEHICLE_NOT_FOUND);

            }
            vehicle.UpdateCurrentMileage(request.CurrentMileage);
            
            

            repository.Update(vehicle);
            await unitOfWork.Commit();

            return mapper.Map<ResponseVehicleJson>(vehicle);
        }
        private void Validate(RequestVehicleUpdateCurrentMileageJson request)
        {
            var validate = new CurrentMiliageValidator();
            var response = validate.Validate(request);
            if (!response.IsValid)
            {
                {
                    throw new ErrorOnValidationException([.. response.Errors.Select(x => x.ErrorMessage)]);
                }
            }

        }

    }
}
