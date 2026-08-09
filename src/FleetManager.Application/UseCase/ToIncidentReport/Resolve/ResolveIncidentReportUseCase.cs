using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToIncidentReport;
using FleetManager.Domain.Repositories.ToVehicle;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToIncidentReport.Resolve
{
    public class ResolveIncidentReportUseCase(IIncidentReportWriteOnlyRepository repository, IVehicleWriteOnlyRepository vehicleRepository , IUnitOfWork unitOfWork) : IResolveIncidentReportUseCase
    {
        public async Task Execute(long id)
        {
            var incidentReport = await repository.GetById(id) ?? throw new NotFoundException(ResourceErrorMessages.INCIDENT_REPORT_NOT_FOUND);
            var vehicle = await vehicleRepository.GetById(incidentReport.VehicleId) ?? throw new NotFoundException(ResourceErrorMessages.VEHICLE_NOT_FOUND);

            incidentReport.Resolve();
            vehicle.UnblockFromIncident();

            vehicleRepository.Update(vehicle);
            repository.Update(incidentReport);
            await unitOfWork.Commit();
        }
    }
}
