namespace FleetManager.Domain.Entities
{
    public class ContractDocument : AuditableEntity
    {
        public long ContractId { get; private set; }
        public long ContractTemplateId { get; private set; }
        public int ContractTemplateVersion { get; private set; }
        public string Content { get; private set; } = string.Empty; // texto já resolvido, congelado
        public DateTime GeneratedAt { get; private set; }

        public Contract Contract { get; internal set; } = default!;

        protected ContractDocument() { }

        public ContractDocument(long contractId, ContractTemplate template, string resolvedContent)
        {
            ContractId = contractId;
            ContractTemplateId = template.Id;
            ContractTemplateVersion = template.Version;
            Content = resolvedContent;
            GeneratedAt = DateTime.UtcNow;
        }
    }
}
