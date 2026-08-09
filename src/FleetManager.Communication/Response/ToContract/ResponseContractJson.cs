using FleetManager.Communication.Response.ToRenant;
using FleetManager.Communication.Response.ToVehicle;

namespace FleetManager.Communication.Response.ToContract
{
    public class ResponseContractJson
    {
        public long Id { get; set; }                  
        public string RentalType { get; set; } = string.Empty;
        public string ContractStatus { get; set; } = string.Empty;
        public DateTime PickupDateTime { get; set; }
        public DateTime ReturnDueDateTime { get; set; }
        public DateTime? ActualReturnDateTime { get; set; }
        public int TotalDays { get; set; }
        public long StartMileage { get; set; }
        public long EndMileage { get; set; }
        public long? FinalMileage { get; set; }
        public decimal? ExcessMileageFee { get; set; }
        public long MileageContracted { get; set; }
        public decimal SnapshotPriceDailyRate { get; set; }
        public decimal SnapshotPriceMonthlyRate { get; set; }
        public decimal SnapshotPricePerExtraMileage { get; set; }
        public decimal TotalAmount { get; set; }
        public ResponseShortVehicleJson Vehicle { get; set; } = new();
        public ResponseTenantJson Tenant { get; set; } = new();
    }
}