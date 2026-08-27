using FleetManager.Domain.Enum;
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

            incidentReport.Resolve();
            repository.Update(incidentReport);

            // Só incidentes de risco Alto bloqueiam o veículo (ver RegisterIncidentReportUseCase);
            // desbloquear incondicionalmente aqui quebraria a resolução de incidentes de risco
            // Baixo, já que o veículo nunca chegou a ser bloqueado por eles.
            if (incidentReport.IncidentRisk == IncidentRisk.High)
            {
                var vehicle = await vehicleRepository.GetById(incidentReport.VehicleId) ?? throw new NotFoundException(ResourceErrorMessages.VEHICLE_NOT_FOUND);
                vehicle.UnblockFromIncident();
                vehicleRepository.Update(vehicle);
            }

            await unitOfWork.Commit();
        }
    }
}
