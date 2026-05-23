using FleetManager.communication.ToEnums;

namespace FleetManager.communication.Requests
{
    public class RequestCategoryJson
    {
        
        public string Name { get; set; } = string.Empty;
        public TransmissionType TransmissionType { get; set; }

    }
}
