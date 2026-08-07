using FleetManager.Communication.Response;
using FleetManager.Communication.Response.ToIncidentReport;

namespace FleetManager.Application.UseCase.ToIncidentReport.GetAll
{
    public interface IGetAllIncidentReportUseCase
    {
        Task<ResponsePaginatedJson<ResponseIncidentReportJson>> Execute(int pageNumber, int pageSize);
    }
}
