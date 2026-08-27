using FleetManager.Communication.Response.ToIncidentReport;

namespace FleetManager.Application.UseCase.ToIncidentReport.GetById
{
    public interface IGetByIdIncidentReportUseCase
    {
        Task<ResponseIncidentReportJson> Execute(long id);
    }
}
