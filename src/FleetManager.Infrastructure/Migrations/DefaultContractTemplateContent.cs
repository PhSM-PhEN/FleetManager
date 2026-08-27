namespace FleetManager.Infrastructure.Migrations
{
    public static class DefaultContractTemplateContent
    {
        public const string Standard = """
            CONTRATO DE LOCAÇÃO DE VEÍCULO

            LOCADOR: [Razão Social da empresa - configurar em Configurações]
            LOCATÁRIO: {{TenantName}}

            CLÁUSULA 1ª - DO OBJETO
            O presente contrato tem por objeto a locação do veículo de placa {{VehiclePlate}}.

            CLÁUSULA 2ª - DO PRAZO
            O prazo de locação é de {{PrazoLocacao}}, com entrega prevista para {{DataEntregaPrevista}}.

            CLÁUSULA 3ª - DA QUILOMETRAGEM
            A quilometragem contratada é de {{QuilometragemContratada}}. A quilometragem excedente
            será cobrada conforme tabela do plano contratado.

            CLÁUSULA 4ª - DO VALOR
            O valor total da locação é de {{ValorTotal}}, a ser pago conforme condições acordadas
            no ato da retirada do veículo.

            CLÁUSULA 5ª - DAS OBRIGAÇÕES
            O LOCATÁRIO se compromete a devolver o veículo nas mesmas condições em que o recebeu,
            respeitando o prazo e a quilometragem contratados.

            E por estarem justas e contratadas, as partes firmam o presente instrumento.
            """;
    }
}
