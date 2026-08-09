using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToIncidentReport;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToIncidentReport.Delete
{
    public class DeleteIncidentReportUseCase(IIncidentReportWriteOnlyRepository repository, IUnitOfWork unitOfWork) : IDeleteIncidentReportUseCase
    {
        public async Task Execute(long id)
        {
            var incidentReport = await repository.GetById(id) ?? 
                        throw new NotFoundException(ResourceErrorMessages.INCIDENT_REPORT_NOT_FOUND);
            await repository.Delete(incidentReport);
            await unitOfWork.Commit();
        }
    }
}
