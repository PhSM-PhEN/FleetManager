using FleetManager.Application.Extensions;
using FleetManager.Communication.Response.ToIncidentReport;
using FleetManager.Domain.Repositories.ToIncidentReport;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToIncidentReport.GetById
{
    public class GetByIdIncidentReportUseCase(IIncidentReportReadOnlyRepository repository) : IGetByIdIncidentReportUseCase
    {
        public async Task<ResponseIncidentReportJson> Execute(long id)
        {
              var incidentReport = await repository.GetById(id) ?? throw new InvalidOperationException(ResourceErrorMessages.INCIDENT_REPORT_NOT_FOUND);

              return incidentReport.ToInfoResponse();
        }
    }
}
