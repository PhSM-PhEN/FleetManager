using FleetManager.Communication.ToEnums;

namespace FleetManager.Communication.Requests
{
    public class RequestCategoryJson
    {
        
        public string Name { get; set; } = string.Empty;
        public TransmissionType TransmissionType { get; set; }

    }
}
