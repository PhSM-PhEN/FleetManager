using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToIncidentReport
{
    public interface IIncidentReportWriteOnlyRepository
    {
        Task Add(IncidentReport incidentReport);
        Task<IncidentReport?>GetById (long id);
        void Update(IncidentReport incidentReport);
        Task Delete(IncidentReport incidentReport);
    }
}
