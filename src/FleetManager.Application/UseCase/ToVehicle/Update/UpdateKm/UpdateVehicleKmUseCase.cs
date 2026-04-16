using AutoMapper;
using FleetManager.communication.Requests;
using FleetManager.communication.Resposnes;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToVehicle;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToVehicle.Update.UpdateKm
{
    public class UpdateVehicleKmUseCase(IUnitOfWork unitOfWork, IVehicleUpdateOnlyRepository repository, IMapper mapper) : IUpdateVehicleKmUseCase
    {

        private readonly IMapper _mapper = mapper;
        private readonly IVehicleUpdateOnlyRepository _repository = repository;
       
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ResponseVehicleJson> Execute(long id, RequestVehicleUpdateCurrentMiliageJson request)
        {
            Validate(request);
            var vehicle = await _repository.GetById(id);

            if(vehicle == null)
            {
                throw new NotFoundException(ResourceErrorMessages.VEHICLE_NOT_FOUND);

            }
            if(request.CurrentMileage < vehicle.CurrentMileage)
            {
                throw new ErrorOnValidationException([ResourceErrorMessages.CURRENT_MILIAGE_MUST_BE_GREATER_THAN_THE_CURRENT]);
            }
            
            _mapper.Map(request, vehicle);
            _repository.Update(vehicle);
            await _unitOfWork.Commit();
            return _mapper.Map<ResponseVehicleJson>(vehicle);   
        }
        private void Validate(RequestVehicleUpdateCurrentMiliageJson request)
        {
            var validate = new CurrentMiliageValidator();
            var response = validate.Validate(request);
            if (!response.IsValid)
            {
                {
                    throw new ErrorOnValidationException(response.Errors.Select(x => x.ErrorMessage).ToList());

                }
            }

        }
    }
}
