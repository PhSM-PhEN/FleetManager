namespace FleetManager.Infrastructure.Migrations
{
    public static class DefaultContractTemplateContent
    {
public const string Standard = """
CONTRATO DE LOCAÇÃO DE VEÍCULO
LOCADOR: {{CompanyName}}, pessoa jurídica de direito privado, inscrita no CNPJ {{CompanyCnpj}}, com sede em {{CompanyAddressCity}}/{{CompanyAddressState}}.
Telefone: {{CompanyPhone}}.
LOCATÁRIO: {{TenantName}}, portador do CPF: {{TenantCpf}} | RG: {{TenantRg}}.
CNH: {{TenantDriverLicense}}/{{TenantDriverLicenseCategory}}.
Contato: {{TenantPhone}}.
Residente e domiciliado na {{TenantAddressStreet}}, nº {{TenantAddressNumber}}, {{TenantAddressCity}}/{{TenantAddressState}}.
CEP: {{TenantAddressZipCode}}.
CLÁUSULA 1ª - DO OBJETO
O presente contrato tem por objeto a locação do veículo abaixo descrito, de propriedade da LOCADORA.
LOCADORA: {{CompanyName}}
Marca/Modelo: {{VehicleBrand}} {{VehicleModel}}
Cor: {{VehicleColor}}   Ano/Modelo: {{VehicleManufacturingYear}}
Placa: {{VehiclePlate}} Chassi: {{VehicleChassis}}  Odômetro: {{VehicleCurrentMileage}} KM 
Data/Hora de retirada: {{StartDate}}
Data/Hora de devolução prevista: {{ExpectedReturnDate}}
CLÁUSULA 2ª - DO PRAZO E VALORES
1. O prazo de locação é de {{RentalPeriod}} ({{RentalPeriodDescription}}) {{RentalPeriodInDays}}, com entrega prevista para {{ExpectedReturnDate}}.
2. O valor total do período é de {{TotalPrice}} ({{TotalPriceInWords}}), a ser quitado no ato da assinatura.
3. A franquia de quilometragem contratada é de {{ContractedMileage}} km. Caso seja ultrapassada, será cobrada uma taxa de {{ExcessKmPrice}} por quilômetro excedente, devendo o débito ser quitado imediatamente na entrega do veículo.
4. Atraso na devolução: caso o veículo seja entregue após o horário previsto, será cobrada automaticamente uma nova diária integral no valor de {{DailyPrice}}, independentemente do número de horas de atraso.
5. Renovação: caso o LOCATÁRIO deseje renovar o período de locação, deverá solicitar a anuência da LOCADORA antes do término do prazo previsto.
CLÁUSULA 3ª - DAS RESPONSABILIDADES DO LOCATÁRIO
1. Conservação: o LOCATÁRIO declara receber o veículo em perfeitas condições de uso, limpeza, conservação e funcionamento, obrigando-se a devolvê-lo no mesmo estado.
2. Danos: o LOCATÁRIO responde integralmente por danos mecânicos, elétricos, estruturais, avarias em pneus, vidros ou lataria ocorridos durante o período de posse.
3. Infrações de trânsito: o LOCATÁRIO é o único responsável por multas ou sanções administrativas aplicadas durante o período da locação.
4. Danos a terceiros: o LOCATÁRIO assume total responsabilidade por danos materiais ou corporais causados a terceiros.
5. Uso indevido: é vedado o uso do veículo para fins ilícitos, competições ou transporte remunerado, salvo autorização expressa.
CLÁUSULA 4ª - DA DEVOLUÇÃO, LIMPEZA E COMBUSTÍVEL
1. Limpeza: o veículo deverá ser entregue higienizado e lavado.
2. Combustível: o LOCATÁRIO obriga-se a devolver o veículo com o mesmo nível de combustível registrado no momento da retirada.
CLÁUSULA 5ª - DO FORO
Para dirimir quaisquer questões decorrentes deste contrato, as partes elegem o Foro da Comarca de {{CompanyAddressCity}}/{{CompanyAddressState}}. {{ContractDate}}
LOCADORA:
{{CompanyName}}
CNPJ: {{CompanyCnpj}}

________________________________

LOCATÁRIO:
{{TenantName}}
CPF: {{TenantCpf}}

________________________________
""";
    }
}