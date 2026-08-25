using FleetManager.Domain.Entities.ValueObjects;
using FleetManager.Domain.Enum;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Domain.Entities
{
    public class Vehicle : AuditableEntity
    {
        public string Brand { get; private set; } = string.Empty;
        public string Model { get; private set; } = string.Empty;
        public string Color { get; private set; } = string.Empty;
        public long CurrentMileage {  get; private set; }
        public ManufacturingYear ManufacturingYear { get; private set; } = default!;
        public Renavam Renavam { get; private set; } = default!;
        public ChassiNumber ChassiNumber { get; private set; } = default!;
        public LicensePlate LicensePlate { get; private set; } = default!;
        public VehicleStatus Status {get ; private set ;} 
        public long CompanyId { get; private set; }
        public long RentalPlanId {get ; private set;}
        public Company Company { get; internal set; } = default!;
        
        public RentalPlan RentalPlan { get; internal set; } = default!;
       
        public IncidentReport? IncidentReport { get; private set; }
        public VehicleStatus GetStatus { get => Status;}

        protected Vehicle() { }

        
        public Vehicle(string brand, string model, string color, ManufacturingYear manufacturing, Renavam renavam,
                       ChassiNumber chassiNumber, LicensePlate licensePlate, long currentMileage, long companyId, long rentalPlanId)
        {
            Brand = brand;
            Model = model;
            Color = color;
            ManufacturingYear = manufacturing;
            Renavam = renavam;
            ChassiNumber = chassiNumber;
            LicensePlate = licensePlate;
            CurrentMileage = currentMileage;
            Status = VehicleStatus.Available;
            CompanyId = companyId;
            RentalPlanId = rentalPlanId;
        }
        public void UpdateMileage(long newMileage)
        {
            if (newMileage < CurrentMileage)
                throw new BusinessRuleException(ResourceErrorMessages.MILEAGE_CANNOT_DECREASE);

            CurrentMileage = newMileage;
            RegisterHistoryEvent("MileageUpdated");
        }

        public void BlockForIncident(IncidentReport incidentReport)
        {
            if (Status == VehicleStatus.BlockedForMaintenance)
                throw new BusinessRuleException(ResourceErrorMessages.VEHICLE_ALREADY_BLOCKED_FOR_MAINTENANCE);
            Status = VehicleStatus.BlockedForMaintenance;
            IncidentReport = incidentReport; 
            RegisterHistoryEvent("BlockedForMaintenance");
        }
        public void UnblockFromIncident()
        {
            if (Status == VehicleStatus.Available)
                throw new BusinessRuleException(ResourceErrorMessages.VEHICLE_NOT_BLOCKED_FOR_MAINTENANCE);
            IncidentReport = null;
            Status = VehicleStatus.Available;
            RegisterHistoryEvent("UnblockedFromMaintenance");
        }
        
        public void Activate()
        {
            if (Status == VehicleStatus.Available)
                throw new BusinessRuleException(ResourceErrorMessages.VEHICLE_ALREADY_ACTIVE);

            Status = VehicleStatus.Available;

            RegisterHistoryEvent("Activated");
        }

        public void Deactivate()
        {
            if (Status == VehicleStatus.Deactivate)
                throw new BusinessRuleException(ResourceErrorMessages.VEHICLE_ALREADY_DEACTIVATED);

            Status = VehicleStatus.Deactivate;
            RegisterHistoryEvent("Deactivated");
        }
    }
}
