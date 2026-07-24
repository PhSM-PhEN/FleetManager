using Bogus;
using FleetManager.Communication.Request.ToVehicle;

namespace CommonTestUtilities.Request.ToVehicle
{
    public class RequestVehicleJsonBuilder
    {
        public static RequestVehicleJson Build(long companyId)
        {
            var faker = new Faker();

            return new Faker<RequestVehicleJson>()
                .RuleFor(request => request.Brand, f => f.Vehicle.Manufacturer())
                .RuleFor(request => request.Model, f => f.Vehicle.Model())
                .RuleFor(request => request.Color, f => f.Commerce.Color())
                .RuleFor(request => request.ManufacturingYear, f => BuildManufacturingYear(f))
                .RuleFor(request => request.Renavam, f => BuildValidRenavam(f))
                .RuleFor(request => request.ChassiNumber, f => f.Random.String2(17, "ABCDEFGHJKLMNPRSTUVWXYZ0123456789"))
                .RuleFor(request => request.LicensePlate, f => BuildMercosulPlate(f))
                .RuleFor(request => request.CurrentMileage, f => f.Random.Long(0, 200_000))
                .RuleFor(request => request.CompanyId, _ => companyId)
                .Generate();
        }

        private static string BuildManufacturingYear(Faker faker)
        {
            var fabricationYear = faker.Date.Past(10).Year;
            var modelYear = faker.Random.Bool() ? fabricationYear : fabricationYear + 1;

            return $"{fabricationYear}/{modelYear}";
        }

        private static string BuildMercosulPlate(Faker faker)
        {
            var letters = faker.Random.String2(3, "ABCDEFGHIJKLMNOPQRSTUVWXYZ");
            var digit = faker.Random.Int(0, 9);
            var letter = faker.Random.String2(1, "ABCDEFGHIJKLMNOPQRSTUVWXYZ");
            var lastDigits = faker.Random.Int(10, 99);

            return $"{letters}{digit}{letter}{lastDigits}";
        }

        // Gera um Renavam com dígito verificador válido, usando o mesmo algoritmo do VO Renavam,
        // senão o teste falharia sempre na validação do próprio Renavam ao montar a entidade/VO.
        private static string BuildValidRenavam(Faker faker)
        {
            var baseDigits = faker.Random.Replace("##########"); // 10 dígitos
            var checkDigit = CalculateCheckDigit(baseDigits);

            return baseDigits + checkDigit;
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