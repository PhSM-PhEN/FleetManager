using FleetManager.Communication.Response.ToRenant;
using FleetManager.Communication.Response.ToVehicle;
using FleetManager.Communication.Response.ToRentalPlan;

namespace FleetManager.Communication.Response.ToContract
{
    public class ResponseContractJson
    {
        public long? Id { get; set; }                   // nulo na prévia, preenchido no registro
        public string RentalType { get; set; } = string.Empty;
        public string ContractStatus { get; set; } = string.Empty;
        public DateTime PickupDateTime { get; set; }
        public DateTime ReturnDueDateTime { get; set; }
        public DateTime? ActualReturnDateTime { get; set; }
        public int TotalDays { get; set; }
        public long StartMileage { get; set; }
        public long EndMileage { get; set; }
        public long MileageContracted { get; set; }
        public decimal SnapshotPriceDailyRate { get; set; }
        public decimal SnapshotPriceMonthlyRate { get; set; }
        public decimal SnapshotPricePerExtraMileage { get; set; }
        public decimal TotalAmount { get; set; }
        public ResponseShortVehicleJson Vehicle { get; set; } = new();
        public ResponseShortTenantJson Tenant { get; set; } = new();
        public ResponseRentalPlanJson RentalPlan { get; set; } = new();
    }
}