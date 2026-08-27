using Bogus;
using FleetManager.Communication.Request.ToMaintenance;

namespace CommonTestUtilities.Request.ToMaintenance
{
    public class RequestMaintenanceJsonBuilder
    {
        public static RequestMaintenanceJson Build(long vehicleId, long? incidentReportId = null, DateTime? scheduledAt = null)
        {
            var faker = new Faker();

            return new Faker<RequestMaintenanceJson>()
                .RuleFor(request => request.VehicleId, _ => vehicleId)
                .RuleFor(request => request.IncidentReportId, _ => incidentReportId)
                .RuleFor(request => request.ScheduledAt, _ => scheduledAt ?? DateTime.UtcNow.AddDays(faker.Random.Int(1, 30)))
                .Generate();
        }
    }
}
