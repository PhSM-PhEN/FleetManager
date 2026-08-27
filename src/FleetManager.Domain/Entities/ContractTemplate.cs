using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Domain.Entities
{
    public class ContractTemplate : AuditableEntity
    {
        public string Name { get; private set; } = string.Empty;
        public int Version { get; private set; }
        public string Content { get; private set; } = string.Empty; // texto com {{Placeholders}}
        public bool IsActive { get; private set; }

        protected ContractTemplate() { }

        public ContractTemplate(string name, string content, int version)
        {
            Name = name;
            Content = content;
            Version = version;
            IsActive = false;
        }

        public void Activate() => IsActive = true;
        public void Deactivate() => IsActive = false;

        public void Update(string name, string content)
        {
            if (IsActive)
                throw new BusinessRuleException(ResourceErrorMessages.CONTRACT_TEMPLATE_ACTIVE_CANNOT_BE_EDITED);

            Name = name;
            Content = content;
            Version += 1;
        }
    }
}
