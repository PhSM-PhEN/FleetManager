using FleetManager.Exception.ExceptionBase;
using System.Text.RegularExpressions;

namespace FleetManager.Domain.Entities.ValueObjects
{
    public class Renavam
    {
        public string Number { get; private set; } = string.Empty;

        protected Renavam() { }

        public Renavam(string number)
        {
            var digitsOnly = OnlyDigits(number);

            if (!IsValid(digitsOnly))
                throw new ErrorOnValidationException([ResourceErrorMessages.RENAVAM_INVALID]);

            Number = digitsOnly;
        }

        private static string OnlyDigits(string value)
        {
            return Regex.Replace(value ?? string.Empty, @"[^\d]", "");
        }

        private static bool IsValid(string renavam)
        {
            
            renavam = renavam.PadLeft(11, '0');

            if (renavam.Length != 11)
                return false;

            if (renavam.Distinct().Count() == 1)
                return false;

            var baseDigits = renavam[..10];
            var checkDigit = CalculateCheckDigit(baseDigits);

            return renavam[10] == checkDigit;
        }

        private static char CalculateCheckDigit(string baseDigits)
        {
            
            var weights = new[] { 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var sum = 0;

            for (var i = 0; i < baseDigits.Length; i++)
            {
                var digit = baseDigits[i] - '0';
                sum += digit * weights[i];
            }

            var remainder = sum % 11;
            var checkDigit = remainder < 2 ? 0 : 11 - remainder;

            return (char)(checkDigit + '0');
        }
    }
}