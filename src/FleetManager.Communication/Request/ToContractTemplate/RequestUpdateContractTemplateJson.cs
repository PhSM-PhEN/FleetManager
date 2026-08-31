namespace FleetManager.Communication.Request.ToContractTemplate
{
    // PATCH parcial: campos nulos significam "não alterar". O front busca o
    // GetById (preview completo) antes e manda só os campos que mudaram.
    public class RequestUpdateContractTemplateJson
    {
        public string? Name { get; set; }
        public string? Content { get; set; }
    }
}
