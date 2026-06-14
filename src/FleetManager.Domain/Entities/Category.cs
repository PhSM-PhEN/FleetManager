using FleetManager.Domain.Enums;

namespace FleetManager.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; private set; } = string.Empty;
        public TransmissionType TransmissionType { get; private set; }
        public bool IsActive { get; private set; } = true;
        public ICollection<Vehicle> Vehicles { get; set; } = [];

        protected Category() { }

        public Category(string name, TransmissionType transmissionType)
        {
            Name = name;
            TransmissionType = transmissionType;
        }

        public void Update(string name, TransmissionType transmissionType)
        {
            Name = name;
            TransmissionType = transmissionType;
        }

        public void Disable() => IsActive = false;
        public void Activate() => IsActive = true;
    }
}
