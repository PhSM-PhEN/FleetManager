using FleetManager.communication.ToEnums;

namespace FleetManager.communication.Responses
{
    public class ResponseShortRentalPlansJson
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public RentalMode RentalMode { get; set; }
        public TransmissionType Transmission { get; set; }
        public decimal PriceRental { get; set; }
        public decimal PricePerKm { get; set; }
        public decimal IncludedKm { get; set; }

    }
}
