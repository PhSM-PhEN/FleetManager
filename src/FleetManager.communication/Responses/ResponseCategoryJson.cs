using FleetManager.communication.ToEnums;

namespace FleetManager.communication.Responses
{
    public class ResponseCategoryJson
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public TransmissionType TransmissionType { get; set; }
        
    }
}
