using Bogus;
using FleetManager.Domain.Entities;

namespace CommonTestUtilities.Entities
{
    public class MaintenanceBuilder
    {
        public static List<Maintenance> Collection(uint count = 3, long? vehicleId = null)
        {
            var list = new List<Maintenance>();
            if (count == 0)
                count = 1;
            var maintenanceId = 1;

            for (var i = 0; i < count; i++)
            {
                var maintenance = Build(vehicleId: vehicleId);
                maintenance.Id = maintenanceId++;

                list.Add(maintenance);
            }

            return list;
        }

        public static Maintenance Build(long? id = null, long? vehicleId = null, long? incidentReportId = null, DateTime? scheduledAt = null)
        {
            var incidentReport = incidentReportId.HasValue
                ? IncidentReportBuilder.Build(id: incidentReportId.Value)
                : null;

            var maintenance = new Faker<Maintenance>()
                .CustomInstantiator(f => new Maintenance(
                    vehicleId ?? f.Random.Long(1, 1000),
                    incidentReport,
                    scheduledAt ?? DateTime.UtcNow.AddDays(f.Random.Int(1, 30))
                ))
                .Generate();

            if (id.HasValue)
                maintenance.Id = id.Value;

            return maintenance;
        }

        public static Maintenance BuildClosed(long? id = null, long? vehicleId = null, long? incidentReportId = null,
            decimal? workshopBudget = null, string? problemDescription = null)
        {
            var faker = new Faker();

            // ScheduledAt no passado só pra deixar o cenário mais realista (manutenção já concluída);
            // Maintenance.Close() não valida ScheduledAt, então isso não afeta o comportamento testado.
            var maintenance = Build(id: id, vehicleId: vehicleId, incidentReportId: incidentReportId,
                scheduledAt: DateTime.UtcNow.AddDays(-faker.Random.Int(1, 10)));

            maintenance.Close(
                problemDescription ?? faker.Lorem.Sentence(),
                workshopBudget ?? faker.Random.Decimal(100, 5000));

            return maintenance;
        }
    }
}
