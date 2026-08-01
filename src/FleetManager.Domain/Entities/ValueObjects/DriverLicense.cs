using FleetManager.Exception.ExceptionBase;
using System.Text.RegularExpressions;

namespace FleetManager.Domain.Entities.ValueObjects
{
    public class DriverLicense
    {
        private static readonly HashSet<string> ValidCategories =
            ["A", "B", "C", "D", "E", "AB", "AC", "AD", "AE"];

        public string Number { get; private set; } = string.Empty;
        public string Category { get; private set; } = string.Empty;

        protected DriverLicense() { }

        public DriverLicense(string number, string category)
        {
            var digitsOnly = Regex.Replace(number ?? string.Empty, @"[^\d]", "");

            if (digitsOnly.Length != 11)
                throw new ErrorOnValidationException([ResourceErrorMessages.DRIVER_LICENSE_NUMBER_INVALID]);

            var normalizedCategory = (category ?? string.Empty).Trim().ToUpperInvariant();

            if (!ValidCategories.Contains(normalizedCategory))
                throw new ErrorOnValidationException([ResourceErrorMessages.DRIVER_LICENSE_CATEGORY_INVALID]);

            Number = digitsOnly;
            Category = normalizedCategory;
        }
    }
}
