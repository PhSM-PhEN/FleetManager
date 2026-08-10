using FleetManager.Application.Extensions;
using FleetManager.Communication.Response;
using FleetManager.Communication.Response.ToIncidentReport;
using FleetManager.Domain.Repositories.ToIncidentReport;

namespace FleetManager.Application.UseCase.ToIncidentReport.GetAll
{
    public class GetAllIncidentReportUseCase(IIncidentReportReadOnlyRepository repository) : IGetAllIncidentReportUseCase
    {
        public async Task<ResponsePaginatedJson<ResponseShortIncidentReportJson>> Execute(int pageNumber, int pageSize)
        {
            if(pageNumber <= 0)
                pageNumber = 1;
            if(pageSize <= 0)
                pageSize = 10;

             var (incidentReport, TotalCount ) = await repository.GetAll(pageNumber, pageSize);

            return new ResponsePaginatedJson<ResponseShortIncidentReportJson>
            {
                Data = incidentReport.ToResponse(),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = TotalCount
            };
        }
    }
}
