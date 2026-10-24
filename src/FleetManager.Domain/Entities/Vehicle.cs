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
        public FuelLevelEnum FuelLevel { get ; private set; }
        public long CompanyId { get; private set; }
        public long RentalPlanId {get ; private set;}
        public Company Company { get; internal set; } = default!;
        public RentalPlan RentalPlan { get; internal set; } = default!;   
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
        public void Deactivate()
        {
            if (Status == VehicleStatus.Inactive)
                throw new BusinessRuleException(ResourceErrorMessages.VEHICLE_ALREADY_DEACTIVATED);

            Status = VehicleStatus.Inactive;
            RegisterHistoryEvent("Inactived");
        }
        public void MarkAsRented()
        {
            if (Status != VehicleStatus.Available)
                throw new BusinessRuleException(ResourceErrorMessages.VEHICLE_ALREADY_RENTED);
            Status = VehicleStatus.Rented;
            RegisterHistoryEvent("Rented");
        }
        public void MarkasAvailable()
        {
            if (Status == VehicleStatus.Available)
                throw new BusinessRuleException(ResourceErrorMessages.VEHICLE_ALREADY_AVAILABLE);

            Status = VehicleStatus.Available;
            RegisterHistoryEvent("Active");
        }
    }
}
