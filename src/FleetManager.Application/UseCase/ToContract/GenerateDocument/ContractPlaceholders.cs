namespace FleetManager.Application.UseCase.ToContract.GenerateDocument
{
    public static class ContractPlaceholders
    {
        // ============================================================
        // LOCADORA
        // ============================================================

        public const string CompanyName = "{{CompanyName}}";
        public const string CompanyCnpj = "{{CompanyCnpj}}";
        public const string CompanyPhone = "{{CompanyPhone}}";

        // Não incluído em "All": não existe campo de e-mail na locadora (ResponseCompanyJson)
        // hoje. Manter fora de "All" até essa informação existir, para o validador de template
        // recusar o uso deste placeholder em vez de deixar o documento falhar ao ser gerado.
        public const string CompanyEmail = "{{CompanyEmail}}";


        // ============================================================
        // LOCATÁRIO
        // ============================================================

        public const string TenantName = "{{TenantName}}";
        public const string TenantCpf = "{{TenantCpf}}";
        public const string TenantRg = "{{TenantRg}}";
        public const string TenantDriverLicense = "{{TenantDriverLicense}}";
        public const string TenantDriverLicenseCategory = "{{TenantDriverLicenseCategory}}";
        public const string TenantPhone = "{{TenantPhone}}";
        public const string TenantEmail = "{{TenantEmail}}";

        // ============================================================
        // ENDEREÇO DA LOCADORA
        // ============================================================

        public const string CompanyAddressStreet = "{{CompanyAddressStreet}}";
        public const string CompanyAddressNumber = "{{CompanyAddressNumber}}";
        public const string CompanyAddressCity = "{{CompanyAddressCity}}";
        public const string CompanyAddressState = "{{CompanyAddressState}}";
        public const string CompanyAddressZipCode = "{{CompanyAddressZipCode}}";


        // ============================================================
        // ENDEREÇO DO LOCATÁRIO
        // ============================================================

        public const string TenantAddressStreet = "{{TenantAddressStreet}}";
        public const string TenantAddressNumber = "{{TenantAddressNumber}}";
        public const string TenantAddressCity = "{{TenantAddressCity}}";
        public const string TenantAddressState = "{{TenantAddressState}}";
        public const string TenantAddressZipCode = "{{TenantAddressZipCode}}";
        // ============================================================
        // VEÍCULO
        // ============================================================

        public const string VehicleBrand = "{{VehicleBrand}}";
        public const string VehicleModel = "{{VehicleModel}}";
        public const string VehicleColor = "{{VehicleColor}}";
        public const string VehicleManufacturingYear = "{{VehicleManufacturingYear}}";
        public const string VehiclePlate = "{{VehiclePlate}}";
        public const string VehicleChassis = "{{VehicleChassis}}";
        public const string VehicleCurrentMileage = "{{VehicleCurrentMileage}}";


        // ============================================================
        // ENTREGA / DEVOLUÇÃO
        // ============================================================

        public const string ExpectedMileageReturn = "{{ExpectedMileageReturn}}";
        public const string MileageAtReturn = "{{MileageAtReturn}}";

        public const string StartDate = "{{StartDate}}";
        public const string ExpectedReturnDate = "{{ExpectedReturnDate}}";


        // ============================================================
        // PRAZO / LOCAÇÃO
        // ============================================================

        public const string RentalPeriod = "{{RentalPeriod}}";
        public const string RentalPeriodInDays = "{{RentalPeriodInDays}}";
        public const string RentalPeriodDescription = "{{RentalPeriodDescription}}";
        


        // ============================================================
        // VALORES
        // ============================================================

        public const string TotalPrice = "{{TotalPrice}}";
        public const string TotalPriceInWords = "{{TotalPriceInWords}}";
        public const string DailyPrice = "{{DailyPrice}}";
        public const string DailyPriceInWords = "{{DailyPriceInWords}}";
        public const string RentalMode = "{{RentalMode}}";


        // ============================================================
        // QUILOMETRAGEM
        // ============================================================

        public const string IncludedKm = "{{IncludedKm}}";

        public const string ExcessKmPrice = "{{ExcessKmPrice}}";
        public const string ExcessKmPriceInWords = "{{ExcessKmPriceInWords}}";

        public const string ContractedMileage = "{{ContractedMileage}}";

        // ============================================================
        // COMBUSTÍVEL
        // ============================================================

        // Não incluído em "All": não existe campo de nível de combustível no domínio (Contract/
        // Vehicle) hoje. Manter fora de "All" até essa informação existir.
        public const string FuelLevelAtStart = "{{FuelLevelAtStart}}";
  
        // ============================================================
        // LOCAL E DATA DO CONTRATO
        // ============================================================

        public const string ContractCity = "{{ContractCity}}";
        public const string ContractState = "{{ContractState}}";
        public const string ContractDate = "{{ContractDate}}";



        // ============================================================
        // STATUS / CONTROLE
        // ============================================================

        public const string ContractNumber = "{{ContractNumber}}";
        public const string ContractStatus = "{{ContractStatus}}";


        // ============================================================
        // COLLECTION COMPLETA
        // ============================================================

        public static readonly string[] All =
        [
            // Locadora
            CompanyName,
            CompanyCnpj,
            CompanyPhone,
            CompanyAddressStreet,
            CompanyAddressNumber,
            CompanyAddressCity,
            CompanyAddressState,
            CompanyAddressZipCode,

            // Locatário
            TenantName,
            TenantCpf,
            TenantRg,
            TenantDriverLicense,
            TenantDriverLicenseCategory,
            TenantPhone,
            TenantEmail,
            TenantAddressCity,
            TenantAddressNumber,
            TenantAddressState,
            TenantAddressStreet,
            TenantAddressZipCode,

            // Veículo
            VehicleBrand,
            VehicleModel,
            VehicleColor,
            VehicleManufacturingYear,
            VehiclePlate,
            VehicleChassis,
            VehicleCurrentMileage,


            // Entrega / devolução
            ExpectedMileageReturn,   
            MileageAtReturn,
            StartDate,   
            ExpectedReturnDate, 


            // Prazo
            RentalPeriod,
            RentalPeriodInDays,
            RentalPeriodDescription,

            // Valores
            TotalPrice,
            TotalPriceInWords,
            DailyPrice,
            DailyPriceInWords,
            RentalMode,

            // Quilometragem
            IncludedKm,
            ExcessKmPrice,
            ExcessKmPriceInWords,
            ContractedMileage,


            // Local/data
            ContractCity,
            ContractState,
            ContractDate,


            // Controle
            ContractNumber,
            ContractStatus
        ];
    }
}