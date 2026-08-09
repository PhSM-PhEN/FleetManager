using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToIncidentReport;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToIncidentReport.Resolve
{
    public class ResolveIncidentReportUseCase(IIncidentReportWriteOnlyRepository repository, IUnitOfWork unitOfWork) : IResolveIncidentReportUseCase
    {
        public async Task Execute(long id)
        {
            var incidentReport = await repository.GetById(id) ?? throw new NotFoundException(ResourceErrorMessages.INCIDENT_REPORT_NOT_FOUND);

            incidentReport.Resolve();

            repository.Update(incidentReport);
            await unitOfWork.Commit();
        }
    }
}
