using FleetManager.Communication.Response;
using FleetManager.Communication.Response.ToIncidentReport;

namespace FleetManager.Application.UseCase.ToIncidentReport.GetAll
{
    public interface IGetAllIncidentReportUseCase
    {
        Task<ResponsePaginatedJson<ResponseShortIncidentReportJson>> Execute(int pageNumber, int pageSize);
    }
}
