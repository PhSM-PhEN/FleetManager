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

        // Vários templates podem estar ativos ao mesmo tempo (ex.: "Locação", "Locação com seguro",
        // "Locação com pagamento parcelado"). Ativar/desativar um não afeta os demais — quem escolhe
        // qual template usar é quem gera o documento do contrato, pelo título/finalidade.
        public void Activate() => IsActive = true;
        public void Deactivate() => IsActive = false;

        // Edição parcial (PATCH): só os campos informados são alterados. Templates ativos também podem
        // ser editados — o documento já gerado fica congelado (Content + Version) em ContractDocument,
        // então mudar o template não altera contratos já gerados, só afeta as próximas gerações.
        public void Update(string? name, string? content)
        {
            if (name is not null)
                Name = name;

            if (content is not null)
            {
                Content = content;
                Version += 1;
            }
        }
    }
}
