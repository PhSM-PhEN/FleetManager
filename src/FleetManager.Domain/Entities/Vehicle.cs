using FleetManager.Domain.Entities.ValueObjects;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Domain.Entities
{
    public class Vehicle : AudiTableEntity
    {
        public string Brand { get; private set; } = string.Empty;
        public string Model { get; private set; } = string.Empty;
        public string Color { get; private set; } = string.Empty;
        public long CurrentMileage {  get; private set; }
        public bool IsActive { get; private set; } = true;
        public ManufacturingYear ManufacturerYear { get; private set; } = default!;
        public Renavam Renavam { get; private set; } = default!;
        public ChassiNumber ChassiNumber { get; private set; } = default!;
        public LicensePlate LicensePlate { get; private set; } = default!;
        public long CompanyId { get; private set; }
        public Company Company { get;  set; } = default!;

        protected Vehicle() { }

        public Vehicle(string brand, string model, string color, ManufacturingYear manufacturing, Renavam renavam,
                       ChassiNumber chassiNumber, LicensePlate licensePlate, long currentMileage, long companyId)
        {
            Brand = brand;
            Model = model;
            Color = color;
            ManufacturerYear = manufacturing;
            Renavam = renavam;
            ChassiNumber = chassiNumber;
            LicensePlate = licensePlate;
            CurrentMileage = currentMileage;
            CompanyId = companyId;
        }
        public void UpdateMileage(long newMileage)
        {
            if (newMileage < CurrentMileage)
                throw new ErrorOnValidationException([ResourceErrorMessages.MILEAGE_CANNOT_DECREASE]);

            CurrentMileage = newMileage;
        }


        public void Activate() => IsActive = true;
        public void Deactivate() => IsActive = false;
    }
}
