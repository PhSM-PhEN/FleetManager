using FleetManager.Application.Extensions;
using FleetManager.Communication.Request.ToMaintenace;
using FleetManager.Communication.Response.ToMaintenance;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToMaintenance;
using FleetManager.Domain.Repositories.ToVehicle;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToMaintenance.Register
{
    public class RegisterMaintenanceUseCase
        (IMaintenanceWriteOnlyRepository repository,
        IVehicleReadOnlyRepository vehicleRepository,
        IUnitOfWork unitOfWork) : IRegisterMaintenaceUseCase
    {
        public async Task<ResponseRegisterMaintenanceJson> Execute(RequestMaintenanceJson request)
        {
            Validate(request);
            _ = await vehicleRepository.GetById(request.VehicleId) ??
                throw new NotFoundException(ResourceErrorMessages.VEHICLE_NOT_FOUND);

            var maintenance = new Maintenance(request.VehicleId, request.IncidentReportId, request.ScheduledAt);
            
            await repository.Add(maintenance);
            await unitOfWork.Commit();

            return maintenance.ToRegisterResponse();
        }
        private static void Validate(RequestMaintenanceJson request)
        {
            var Validator = new MaintenanceValidator();
            var result = Validator.Validate(request);

            if(result.IsValid == false)
            {
                var errors = result.Errors.Select(erros => erros.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errors);
            }
        }
    }
}
