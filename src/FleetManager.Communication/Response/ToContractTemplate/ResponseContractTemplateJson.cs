namespace FleetManager.Communication.Response.ToContractTemplate
{
    public class ResponseContractTemplateJson
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int Version { get; set; }
        public bool IsActive { get; set; }
    }
}
