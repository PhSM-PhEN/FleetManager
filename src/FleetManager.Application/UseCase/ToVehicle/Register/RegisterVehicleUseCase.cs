using FleetManager.Application.Extensions;
using FleetManager.Communication.Requests;
using FleetManager.Communication.Responses;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToCategory;
using FleetManager.Domain.Repositories.ToVehicle;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToVehicle.Register
{
    public class RegisterVehicleUseCase(IVehicleWriteOnlyRepository vehicleWriteOnly,
            IUnitOfWork unitOfWork, ICategoryReadOnlyRepository categoryGetById) : IRegisterVehicleUseCase
    {

        public async Task<ResponseRegisterVehicleJson> Execute(RequestVehicleJson request)
        {

            Validate(request);
            await ValidateCategory(request.CategoryId);


            var vehicle =new Vehicle(request.Brand, request.Model, request.ManufacturingYear, request.Renavam,
            request.ChassisNumber, request.LicensePlate, request.Color, request.CategoryId, request.CurrentMileage);


            await vehicleWriteOnly.Add(vehicle);

            await unitOfWork.Commit();

            return vehicle.ToRegisterResponse();
        }
        private static void Validate(RequestVehicleJson request)
        {
            var validator = new VehicleValidator();
            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errorMessage = result.Errors.Select(erros => erros.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessage);
            }
        }
        private async Task ValidateCategory(long categoryId)
        {


            var result = await categoryGetById.GetById(categoryId);

            if (result is null)
            {
                throw new  NotFoundException(ResourceErrorMessages.CATEGORY_NOT_FOUND);
            }
    
        }
    }
}
