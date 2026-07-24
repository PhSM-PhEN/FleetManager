using FleetManager.Exception.ExceptionBase;
using System.Text.RegularExpressions;

namespace FleetManager.Domain.Entities.ValueObjects
{
    public class ChassiNumber
    {
        // 17 caracteres alfanuméricos, sem I, O, Q (padrão ISO 3779 / VIN)
        private static readonly Regex Format = new(@"^[A-HJ-NPR-Z0-9]{17}$");

        public string Number { get; private set; } = string.Empty;

        protected ChassiNumber() { }

        public ChassiNumber(string number)
        {
            var normalized = Normalize(number);

            if (!Format.IsMatch(normalized))
                throw new ErrorOnValidationException([ResourceErrorMessages.CHASSI_NUMBER_INVALID]);

            Number = normalized;
        }

        private static string Normalize(string value)
        {
            return (value ?? string.Empty).Trim().ToUpperInvariant();
        }
    }
}