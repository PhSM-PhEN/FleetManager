using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToIncidentReport;
using Moq;

namespace CommonTestUtilities.Repositories.ToIncidentReport
{
    public class IncidentReportWriteOnlyRepositoryBuilder
    {
        private readonly Mock<IIncidentReportWriteOnlyRepository> _repository;

        public IncidentReportWriteOnlyRepositoryBuilder()
        {
            _repository = new Mock<IIncidentReportWriteOnlyRepository>();
        }

        public IncidentReportWriteOnlyRepositoryBuilder Add()
        {
            _repository.Setup(r => r.Add(It.IsAny<IncidentReport>())).Returns(Task.CompletedTask);
            return this;
        }

        public IncidentReportWriteOnlyRepositoryBuilder GetById(long id, IncidentReport? incidentReport)
        {
            _repository.Setup(r => r.GetById(id)).ReturnsAsync(incidentReport);
            return this;
        }

        public IncidentReportWriteOnlyRepositoryBuilder Update(IncidentReport incidentReport)
        {
            _repository.Setup(r => r.Update(incidentReport));
            return this;
        }

        public IncidentReportWriteOnlyRepositoryBuilder Delete(IncidentReport incidentReport)
        {
            _repository.Setup(r => r.Delete(incidentReport)).Returns(Task.CompletedTask);
            return this;
        }

        public Mock<IIncidentReportWriteOnlyRepository> BuildMock() => _repository;

        public IIncidentReportWriteOnlyRepository Build() => _repository.Object;
    }
}
