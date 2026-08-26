using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToIncidentReport;
using Moq;

namespace CommonTestUtilities.Repositories.ToIncidentReport
{
    public class IncidentReportReadOnlyRepositoryBuilder
    {
        private readonly Mock<IIncidentReportReadOnlyRepository> _repository;

        public IncidentReportReadOnlyRepositoryBuilder()
        {
            _repository = new Mock<IIncidentReportReadOnlyRepository>();
        }

        public IncidentReportReadOnlyRepositoryBuilder GetAll(List<IncidentReport> incidentReports, int pageNumber, int pageSize, int totalCount)
        {
            _repository.Setup(r => r.GetAll(pageNumber, pageSize)).ReturnsAsync((incidentReports, totalCount));
            return this;
        }

        public IncidentReportReadOnlyRepositoryBuilder GetById(long id, IncidentReport? incidentReport)
        {
            _repository.Setup(r => r.GetById(id)).ReturnsAsync(incidentReport);
            return this;
        }

        public Mock<IIncidentReportReadOnlyRepository> BuildMock() => _repository;

        public IIncidentReportReadOnlyRepository Build() => _repository.Object;
    }
}
