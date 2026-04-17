using AutoMapper;
using FleetManager.Application.UseCase.ToCategory.GetById;
using FleetManager.communication.Requests.ToVehicle;
using FleetManager.communication.Resposnes.ToVehicle;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToCategory;
using FleetManager.Domain.Repositories.ToVehicle;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToVehicle.Register
{
    public class RegisterVehicleUseCase(IMapper mapper,
            IVehicleWriteOnlyRepository vehicleWriteOnly,
            IUnitOfWork unitOfWork,
            ICategoryReadOnlyRepository categoryGetById) : IRegisterVehicleUseCase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IVehicleWriteOnlyRepository _vehicleWriteOnly = vehicleWriteOnly;
        private readonly ICategoryReadOnlyRepository _categoryGetById = categoryGetById;
        private readonly IMapper _mapper = mapper;
        public async Task<ResponseRegisterVehicleJson> Execute(RequestVehicleJson request)
        {

            Validate(request);
            await ValidateCategory(request.CategoryId);
            

            var vehicle = _mapper.Map<Vehicle>(request);
            

            await _vehicleWriteOnly.Add(vehicle);

            await _unitOfWork.Commit();

            return _mapper.Map<ResponseRegisterVehicleJson>(vehicle);
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
        private async Task ValidateCategory(int categoryId)
        {


            var result = await _categoryGetById.GetById(categoryId);

            if (result is null)
            {
                throw new  NotFoundException(ResourceErrorMessages.CATEGORY_NOT_FOUND);
            }
    
        }
    }
}
