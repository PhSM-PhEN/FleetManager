using FleetManager.Domain.Entities.ValueObjects;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Domain.Entities
{
    public class Vehicle : AuditableEntity
    {
        public string Brand { get; private set; } = string.Empty;
        public string Model { get; private set; } = string.Empty;
        public string Color { get; private set; } = string.Empty;
        public long CurrentMileage {  get; private set; }
        public bool IsActive { get; private set; } = true;
        public ManufacturingYear ManufacturingYear { get; private set; } = default!;
        public Renavam Renavam { get; private set; } = default!;
        public ChassiNumber ChassiNumber { get; private set; } = default!;
        public LicensePlate LicensePlate { get; private set; } = default!;
        public long CompanyId { get; private set; }
        public long RentalPlanId {get ; private set;}
        public Company Company { get; internal set; } = default!;
        public RentalPlan RentalPlan { get; internal set; } = default!;
       
        public IncidentReport? BlockingIncidentReport { get; private set; }
        public bool IsBlockedForMaintenance => BlockingIncidentReport is not null;

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
            if (IsBlockedForMaintenance)
                throw new BusinessRuleException(ResourceErrorMessages.VEHICLE_ALREADY_BLOCKED_FOR_MAINTENANCE);

            BlockingIncidentReport = incidentReport; 
            RegisterHistoryEvent("BlockedForMaintenance");
        }
        public void UnblockFromIncident()
        {
            if (!IsBlockedForMaintenance)
                throw new BusinessRuleException(ResourceErrorMessages.VEHICLE_NOT_BLOCKED_FOR_MAINTENANCE);
            BlockingIncidentReport = null;
            RegisterHistoryEvent("UnblockedFromMaintenance");
        }
        public void Activate()
        {
            IsActive = true;
            RegisterHistoryEvent("Activated");
        }

        public void Deactivate()
        {
            IsActive = false;
            RegisterHistoryEvent("Deactivated");
        }
    }
}
