namespace FleetManager.Infrastructure.Migrations
{
    public static class DefaultContractTemplateContent
    {
        public const string Standard = """
            CONTRATO DE LOCAÇÃO DE VEÍCULO

            LOCADOR: {{CompanyName}}, pessoa jurídica de direito privado, inscrita no CNPJ {{CompanyCnpj}}
            com sede em {{CompanyAddressCity}}/{{CompanyAddressState}}.
            Telefone: {{CompanyPhone}}

            LOCATÁRIO: {{TenantName}}, portador do CPF:{{TenantCpf}} | RG:{{TenantRg}} 
            CNH {{TenantDriverLicense}}/{{TenantDriverLicenseCategory}}. Contato: {{TenantPhone}}
            Residente e domiciliado na {{TenantAddressStreet}} - N° {{TenantAddressNumber}} - {{TenantAddressCity}}/{{TenantAddressState}}
            CEP: {{TenantAddressZipCode}}.

            CLÁUSULA 1ª - DO OBJETO

            O presente contrato tem por objeto a locação do veículo abaixo descrito, de propriedade da locadora.
            {{CompanyName}}
              Modelo: {{VehicleBrand}} {{VehicleModel}} | Cor: {{VehicleColor}} | Ano/Modelo: {{VehicleManufacturingYear}}
              Placa : {{VehiclePlate}} | {{VehicleChassis}} | Odômetro : {{VehicleCurrentMileage}} 
              Data/Hora de retirada: {{StartDate}} || Data/Hora de devoluçao: {{ExpectedReturnDate}} 
            
            CLÁUSULA 2ª - DO PRAZO E VALORES

            1. O prazo de locação é de {{RentalPeriod}} ({{RentalPeriodDescription}}) {{RentalPeriodInDays}}, com entrega prevista para {{ExpectedReturnDate}}.
            2. O valor total do periodo é de {{TotalPrice}} ({{TotalPriceInWords}}) quitados no ato  da assinatura.
            3. Franquia de quilometro contratado é de {{ContractedMileage}}. Caso ultrapassado sera cobrado uma taxa de {{ExcessKmPrice}} por quilometro excedente,
               devendo o debito ser quitado imediatamente na entrega do veiculo.
            4. Atraso na Devolução: Caso o veículo seja entregue após o horário previsto no item 1 desta cláusula, 
               será cobrada automaticamente uma nova diária integral no valor de {{DailyPrice}}, independentemente do número de horas de atraso, 
               cujo valor deverá ser quitado imediatamente no ato da entrega
            5. Renovação: Caso o LOCATÁRIO deseje renovar o período de locação, deverá solicitar a anuência do LOCADOR antes do término do prazo previsto no item 1.
               Havendo concordância e a confirmação do pagamento referente ao novo período, 
               os termos deste contrato permanecerão válidos e em pleno vigor até a nova data de entrega estipulada.

            CLÁUSULA 3ª - DAS  RESPONSABILIDADES DO LOCATÁRIO

            1. Conservação: O LOCATÁRIO declara receber o veículo em perfeitas condições de uso, limpeza, conservação e funcionamento, obrigando-se a devolvê-lo no mesmo estado.
            2. Danos: O LOCATÁRIO responde integralmente por danos mecânicos, elétricos, estruturais, avarias em pneus, vidros ou lataria ocorridos durante o período de posse.
            3. Infrações de Trânsito: O LOCATÁRIO é o único responsável por multas ou sanções administrativas aplicadas no período da locação, autorizando desde já a LOCADORA a realizar a indicação de condutor junto ao DETRAN e a cobrança dos valores correspondentes.
            4. Danos a Terceiros: O LOCATÁRIO assume total responsabilidade por danos materiais ou corporais causados a terceiros, isentando a LOCADORA de qualquer litígio.
            5. Uso Indevido: É vedado o uso do veículo para fins ilícitos, competições ou transporte remunerado, salvo autorização expressa.

            CLÁUSULA 4ª DEVOLUÇÃO, LIMPEZA E COMBUSTÍVEL

            1. Limpeza: O veículo deverá ser entregue higienizado e lavado. A não observância da limpeza padrão resultará na cobrança de uma taxa de lavagem técnica a ser definida pela LOCADORA.
            2. Combustível: O LOCATÁRIO obriga-se a devolver o veículo com o mesmo nível de combustível registrado no momento da retirada. Caso o nível seja inferior, será cobrado o valor do combustível para reposição acrescido de taxa de serviço.

            CLÁUSULA 5ª DO FORO
            Para dirimir quaisquer questões decorrentes deste contrato, as partes elegem o Foro da Comarca de {{CompanyAddressCity}}/{{CompanyAddressState}}.
            {{ContractDate}}


            {{CompanyName}}                                                             {{TenantName}}
            {{CompanyCnpj}}                                                             {{TenantCpf}}


            ________________________________                                            _________________________________
            """;
    }
}