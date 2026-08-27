namespace FleetManager.Communication.Response.ToContract
{
    public class ResponseContractDocumentJson
    {
        public long ContractId {get ; set ;}
        public int TemplateVersion { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime GeneratedAt { get; set; }
        public ResponseContractJson Contract {get ; set ;} = new();
    }
}
