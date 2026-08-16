using System.Reflection;
using FleetManager.Domain.Entities;

namespace CommonTestUtilities.Entities
{
    /// <summary>
    /// Maintenance.Vehicle e Maintenance.IncidentReport são declaradas com setter `private`
    /// (diferente de Contract.Vehicle / Vehicle.Company, que usam `internal set` justamente
    /// para permitir que o CommonTestUtilities as popule via InternalsVisibleTo).
    /// Como o assembly de testes não tem acesso a membros `private` de outra classe,
    /// usamos reflection aqui como workaround só para testes que precisam exercitar
    /// código que lê essas navegações (ex: GetByIdMaintenanceUseCase / ToInfoResponse).
    /// Idealmente Maintenance.Vehicle/IncidentReport deveriam virar `internal set`,
    /// alinhando com o padrão já usado por Contract e Vehicle.
    /// </summary>
    public static class MaintenanceNavigationExtensions
    {
        public static Maintenance WithVehicle(this Maintenance maintenance, Vehicle vehicle)
        {
            SetPrivateProperty(maintenance, nameof(Maintenance.Vehicle), vehicle);
            return maintenance;
        }

        public static Maintenance WithIncidentReport(this Maintenance maintenance, IncidentReport incidentReport)
        {
            SetPrivateProperty(maintenance, nameof(Maintenance.IncidentReport), incidentReport);
            return maintenance;
        }

        private static void SetPrivateProperty(object target, string propertyName, object value)
        {
            var property = target.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance)
                ?? throw new InvalidOperationException($"Property '{propertyName}' not found on '{target.GetType().Name}'.");

            property.SetValue(target, value);
        }
    }
}
