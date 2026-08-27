namespace FleetManager.Application.UseCase.ToContract.GenerateDocument
{
    public static class ContractPlaceholders
    {
        public const string TenantName = "{{TenantName}}";
        public const string VehiclePlate = "{{VehiclePlate}}";
        public const string PrazoLocacao = "{{PrazoLocacao}}";
        public const string DataEntregaPrevista = "{{DataEntregaPrevista}}";
        public const string QuilometragemContratada = "{{QuilometragemContratada}}";
        public const string ValorTotal = "{{ValorTotal}}";

        public static readonly string[] All =
        [
            TenantName, VehiclePlate,
            PrazoLocacao, DataEntregaPrevista, QuilometragemContratada, ValorTotal
        ];
    }
}
