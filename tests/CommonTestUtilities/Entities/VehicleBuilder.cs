using Bogus;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Entities.ValueObjects;

namespace CommonTestUtilities.Entities
{
    public class VehicleBuilder
    {
        public static List<Vehicle> Collection(uint count = 3, long? companyId = null)
        {
            var list = new List<Vehicle>();
            if (count == 0)
                count = 1;
            var vehicleId = 1;

            for (var i = 0; i < count; i++)
            {
                var vehicle = Build(companyId: companyId);
                vehicle.Id = vehicleId++;
                list.Add(vehicle);
            }

            return list;
        }

        public static Vehicle Build(int? id = null, long? companyId = null)
        {
            var vehicle = new Faker<Vehicle>()
                .CustomInstantiator(f => new Vehicle(
                    f.Vehicle.Manufacturer(),
                    f.Vehicle.Model(),
                    f.Commerce.Color(),
                    BuildManufacturingYear(f),
                    BuildValidRenavam(f),
                    new ChassiNumber(f.Random.String2(17, "ABCDEFGHJKLMNPRSTUVWXYZ0123456789")),
                    BuildMercosulPlate(f),
                    f.Random.Long(0, 200_000),
                    companyId ?? f.Random.Long(1, 1000)
                ))
                .Generate();

            if (id.HasValue)
                vehicle.Id = id.Value;

            return vehicle;
        }

        private static ManufacturingYear BuildManufacturingYear(Faker faker)
        {
            var fabricationYear = faker.Date.Past(10).Year;
            var modelYear = faker.Random.Bool() ? fabricationYear : fabricationYear + 1;

            return new ManufacturingYear(fabricationYear, modelYear);
        }

        private static LicensePlate BuildMercosulPlate(Faker faker)
        {
            var letters = faker.Random.String2(3, "ABCDEFGHIJKLMNOPQRSTUVWXYZ");
            var digit = faker.Random.Int(0, 9);
            var letter = faker.Random.String2(1, "ABCDEFGHIJKLMNOPQRSTUVWXYZ");
            var lastDigits = faker.Random.Int(10, 99);

            return new LicensePlate($"{letters}{digit}{letter}{lastDigits}");
        }

        // Mesmo algoritmo do VO Renavam — precisa gerar um valor que já nasça válido,
        // já que o construtor de Renavam valida o dígito verificador.
        private static Renavam BuildValidRenavam(Faker faker)
        {
            var baseDigits = faker.Random.Replace("##########"); // 10 dígitos
            var checkDigit = CalculateCheckDigit(baseDigits);

            return new Renavam(baseDigits + checkDigit);
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