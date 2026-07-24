using FleetManager.Exception.ExceptionBase;
using System.Text.RegularExpressions;

namespace FleetManager.Domain.Entities.ValueObjects
{
    public class LicensePlate
    {
        private static readonly Regex OldFormat = new(@"^[A-Z]{3}-?\d{4}$");
        private static readonly Regex MercosulFormat = new(@"^[A-Z]{3}\d[A-Z]\d{2}$");

        public string Number { get; private set; } = string.Empty;
        public bool IsMercosul { get; private set; }

        protected LicensePlate() { }

        public LicensePlate(string plate)
        {
            var normalized = Normalize(plate);

            if (MercosulFormat.IsMatch(normalized))
            {
                Number = normalized;
                IsMercosul = true;
                return;
            }

            if (OldFormat.IsMatch(normalized))
            {
                Number = normalized;
                IsMercosul = false;
                return;
            }

            throw new ErrorOnValidationException([ResourceErrorMessages.LICENSE_PLATE_INVALID]);
        }

        private static string Normalize(string value)
        {
            return (value ?? string.Empty).Trim().ToUpperInvariant().Replace(" ", "");
        }

        public override string ToString() => Number;
    }
}