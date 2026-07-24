using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Domain.Entities.ValueObjects
{
    public class ManufacturingYear
    {
        private const int MinimumYear = 1950;

        public int FabricationYear { get; }
        public int ModelYear { get; }

        public ManufacturingYear(int fabricationYear, int modelYear)
        {
            var currentYear = DateTime.UtcNow.Year;

            if (fabricationYear < MinimumYear || fabricationYear > currentYear + 1)
                throw new ErrorOnValidationException([ResourceErrorMessages.FABRICATION_YEAR_INVALID]);

            if (modelYear < fabricationYear || modelYear > fabricationYear + 1)
                throw new ErrorOnValidationException([ResourceErrorMessages.MODEL_YEAR_INVALID]);

            FabricationYear = fabricationYear;
            ModelYear = modelYear;
        }

        // Aceita o formato exato do documento: "2024/2025" ou "2024/2024"
        public static ManufacturingYear Parse(string value)
        {
            var parts = value?.Split('/') ?? [];

            if (parts.Length != 2
                || !int.TryParse(parts[0], out var fabricationYear)
                || !int.TryParse(parts[1], out var modelYear))
            {
                throw new ErrorOnValidationException([ResourceErrorMessages.MANUFACTURING_YEAR_FORMAT_INVALID]);
            }

            return new ManufacturingYear(fabricationYear, modelYear);
        }

        public override string ToString() => $"{FabricationYear}/{ModelYear}";
    }
}