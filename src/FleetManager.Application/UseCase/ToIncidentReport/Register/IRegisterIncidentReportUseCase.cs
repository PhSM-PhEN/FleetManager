using FleetManager.Communication.Request.ToIncidentReport;
using FleetManager.Communication.Response.ToIncidentReport;

namespace FleetManager.Application.UseCase.ToIncidentReport.Register
{
    public interface IRegisterIncidentReportUseCase
    {
        Task<ResponseShortIncidentReportJson> Execute(RequestIncidentReportJson request);
    }
}
