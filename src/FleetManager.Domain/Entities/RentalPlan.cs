using FleetManager.Domain.Enums;

namespace FleetManager.Domain.Entities
{
    public class RentalPlan
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public RentalMode Mode { get; set; }
        public TransmissionType Transmission { get; set; }
        public decimal PriceRental { get;  set; }
        public decimal PricePerKm { get;  set; }
        public bool IsActive { get; set; } = true;



              
    }
}
