using FleetManager.Application.Extensions;
using FleetManager.Communication.Request.ToMaintenance;
using FleetManager.Communication.Response.ToMaintenance;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToIncidentReport;
using FleetManager.Domain.Repositories.ToMaintenance;
using FleetManager.Domain.Repositories.ToVehicle;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToMaintenance.Register
{
    public class RegisterMaintenanceUseCase
        (IMaintenanceWriteOnlyRepository repository,
        IVehicleReadOnlyRepository vehicleRepository,
        IIncidentReportReadOnlyRepository incidentReportRepository,
        IUnitOfWork unitOfWork) : IRegisterMaintenanceUseCase
    {
        public async Task<ResponseShortMaintenanceJson> Execute(RequestMaintenanceJson request)
        {
            Validate(request);
            _ = await vehicleRepository.GetById(request.VehicleId) ??
                throw new NotFoundException(ResourceErrorMessages.VEHICLE_NOT_FOUND);
                
            var incidentReport = request.IncidentReportId.HasValue
                ? await incidentReportRepository.GetById(request.IncidentReportId.Value) ??
                    throw new NotFoundException(ResourceErrorMessages.INCIDENT_REPORT_NOT_FOUND)
                : null;

            var maintenance = new Maintenance(request.VehicleId, request.ServiceCenter ,incidentReport, request.ScheduledAt);
            
            await repository.Add(maintenance);
            await unitOfWork.Commit();

            return maintenance.ToResponse();
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
