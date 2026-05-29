using FleetManager.communication.ToEnums;

namespace FleetManager.communication.Responses
{
    public class ResponseRentalPlanJson
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public RentalMode Mode { get; set; }
        public TransmissionType Transmission { get; set; }
        public decimal PriceRental { get; private set; }
        public decimal PricePerKm { get; private set; }
       
        
    }
}
