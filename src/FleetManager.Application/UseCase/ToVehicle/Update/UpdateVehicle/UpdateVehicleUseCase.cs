using AutoMapper;
using FleetManager.communication.Requests;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToVehicle;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToVehicle.Update.UpdateVehicle
{
    public class UpdateVehicleUseCase(IUnitOfWork unitOfWork, IVehicleUpdateOnlyRepository repository, IMapper mapper) : IUpdateVehicleUseCase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IVehicleUpdateOnlyRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task Execute(long id, RequestVehicleJson request)
        {
            Validate(request);
            var vehicle = await _repository.GetById(id);
            if (vehicle == null)
            {
                throw new NotFoundException(ResourceErrorMessages.VEHICLE_NOT_FOUND);
            }

            _mapper.Map(request, vehicle);

             _repository.Update(vehicle);

            await _unitOfWork.Commit();
        }
        private static void Validate(RequestVehicleJson request)
        {
            var validator = new VehicleValidator();
            var result = validator.Validate(request);
            if (result.IsValid == false)
            {
                var errorMessage = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessage);
            }
        }
    }
}
