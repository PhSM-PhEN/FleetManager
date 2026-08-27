using Bogus;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Enum;

namespace CommonTestUtilities.Entities
{
    public class IncidentReportBuilder
    {
        public static List<IncidentReport> Collection(uint count = 3, long? contractId = null, long? vehicleId = null)
        {
            var list = new List<IncidentReport>();
            if (count == 0)
                count = 1;
            var incidentReportId = 1;

            for (var i = 0; i < count; i++)
            {
                var incidentReport = Build(contractId: contractId, vehicleId: vehicleId);
                incidentReport.Id = incidentReportId++;

                list.Add(incidentReport);
            }

            return list;
        }

        public static IncidentReport Build(long? id = null, long? contractId = null, long? vehicleId = null,
            string? description = null, IncidentRisk? incidentRisk = null)
        {
            var incidentReport = new Faker<IncidentReport>()
                .CustomInstantiator(f => new IncidentReport(
                    contractId ?? f.Random.Long(1, 1000),
                    vehicleId ?? f.Random.Long(1, 1000),
                    description ?? f.Lorem.Sentence(),
                    incidentRisk ?? f.PickRandom<IncidentRisk>()
                ))
                .Generate();

            if (id.HasValue)
                incidentReport.Id = id.Value;

            return incidentReport;
        }
    }
}
