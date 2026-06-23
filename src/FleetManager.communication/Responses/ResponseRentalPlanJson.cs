using FleetManager.Communication.ToEnums;

namespace FleetManager.Communication.Responses
{
    public class ResponseRentalPlanJson
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public RentalMode Mode { get; set; }
        public TransmissionType Transmission { get; set; }
        public decimal PriceRental { get; private set; }
        public decimal PricePerKm { get; private set; }
       
        
    }
}
