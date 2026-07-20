using FleetManager.Exception.ExceptionBase;
using System.Text.RegularExpressions;
namespace FleetManager.Domain.Entities.ValueObjects
{
    public class Cpf
    {
        public string Number { get; private set; } = string.Empty;

        protected Cpf() { }

        public Cpf(string number)
        {
            var digitsOnly = OnlyDigits(number);

            if (!IsValid(digitsOnly))
                throw new ErrorOnValidationException([ResourceErrorMessages.CPF_INVALID]);

            Number = digitsOnly;
        }

        private static string OnlyDigits(string value)
        {
            return Regex.Replace(value ?? string.Empty, @"[^\d]", "");
        }

        private static bool IsValid(string cpf)
        {
            // Precisa ter 11 dígitos
            if (cpf.Length != 11)
                return false;

            if (cpf.Distinct().Count() == 1)
                return false;

            var firstCheckDigit = CalculateCheckDigit(cpf[..9], startingWeight: 10);
            var secondCheckDigit = CalculateCheckDigit(cpf[..9] + firstCheckDigit, startingWeight: 11);

            return cpf[9] == firstCheckDigit && cpf[10] == secondCheckDigit;
        }

        private static char CalculateCheckDigit(string baseDigits, int startingWeight)
        {
            var sum = 0;
            var weight = startingWeight;

            foreach (var digitChar in baseDigits)
            {
                var digit = digitChar - '0'; 
                sum += digit * weight;
                weight--;
            }

            var remainder = sum % 11;
            var checkDigit = remainder < 2 ? 0 : 11 - remainder;

            return (char)(checkDigit + '0'); 
        }

    }
}
