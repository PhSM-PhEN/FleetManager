using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToIncidentReport
{
    public interface IIncidentReportReadOnlyRepository
    {
        Task<IncidentReport?> GetById(long id);
        Task<(List<IncidentReport>,int TotalCount )> GetAll(int pageNumber, int PageSize);
    }
}
