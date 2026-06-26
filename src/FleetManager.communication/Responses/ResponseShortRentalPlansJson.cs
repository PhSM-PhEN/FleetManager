using FleetManager.Communication.ToEnums;

namespace FleetManager.Communication.Responses
{
    public class ResponseShortRentalPlansJson
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public RentalMode RentalMode { get; set; }
        public TransmissionType Transmission { get; set; }

    }
}
