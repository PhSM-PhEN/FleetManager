using FleetManager.Communication.Response.ToContract;
using System.Globalization;

namespace FleetManager.Application.UseCase.ToContract.GenerateDocument
{
    public static class ContractDocumentPlaceholderResolver
    {
        private static readonly CultureInfo Culture = new("pt-BR");

        public static string Resolve(string templateContent, ResponseContractJson contract)
        {
            var values = new Dictionary<string, string>
            {
                [ContractPlaceholders.CompanyName] = contract.Vehicle.Company.Name,
                [ContractPlaceholders.CompanyCnpj] = contract.Vehicle.Company.Cnpj,
                [ContractPlaceholders.CompanyPhone] = contract.Vehicle.Company.PhoneNumber,
                [ContractPlaceholders.CompanyAddressCity] = contract.Vehicle.Company.Address.City,
                [ContractPlaceholders.CompanyAddressStreet] = contract.Vehicle.Company.Address.Street,
                [ContractPlaceholders.CompanyAddressNumber] = contract.Vehicle.Company.Address.Number,
                [ContractPlaceholders.CompanyAddressState] = contract.Vehicle.Company.Address.State,
                [ContractPlaceholders.CompanyAddressZipCode] = contract.Vehicle.Company.Address.ZipCode,
                [ContractPlaceholders.TenantName] = contract.Tenant.Name,
                [ContractPlaceholders.TenantCpf] = contract.Tenant.Cpf,
                [ContractPlaceholders.TenantRg] = contract.Tenant.RG,
                [ContractPlaceholders.TenantDriverLicense] = contract.Tenant.DriverLicenseNumber,
                [ContractPlaceholders.TenantDriverLicenseCategory] = contract.Tenant.DriverLicenseCategory,
                [ContractPlaceholders.TenantPhone] = contract.Tenant.PhoneNumber,
                [ContractPlaceholders.TenantEmail] = contract.Tenant.Email ?? string.Empty,
                [ContractPlaceholders.TenantAddressStreet] = contract.Tenant.Address.Street,
                [ContractPlaceholders.TenantAddressNumber] = contract.Tenant.Address.Number,
                [ContractPlaceholders.TenantAddressCity] = contract.Tenant.Address.City,
                [ContractPlaceholders.TenantAddressState] = contract.Tenant.Address.State,
                [ContractPlaceholders.TenantAddressZipCode] = contract.Tenant.Address.ZipCode,

                [ContractPlaceholders.VehiclePlate] = contract.Vehicle.LicensePlate,
                [ContractPlaceholders.VehicleBrand] = contract.Vehicle.Brand,
                [ContractPlaceholders.VehicleColor] = contract.Vehicle.Color,
                [ContractPlaceholders.VehicleManufacturingYear] = contract.Vehicle.ManufacturingYear,
                [ContractPlaceholders.VehicleChassis] = contract.Vehicle.ChassiNumber,
                [ContractPlaceholders.VehicleModel] = contract.Vehicle.Model,
                [ContractPlaceholders.VehicleCurrentMileage] = contract.Vehicle.CurrentMileage.ToString(),
                [ContractPlaceholders.ExpectedMileageReturn] = contract.ExpectedEndMileage.ToString(),
                [ContractPlaceholders.MileageAtReturn] = contract.FinalMileage.HasValue
                    ? FormatMileage(contract.FinalMileage.Value)
                    : "—",

                [ContractPlaceholders.StartDate] = contract.PickupDateTime.ToString("dd/MM/yyyy HH:mm"),
                [ContractPlaceholders.ExpectedReturnDate] = contract.ReturnDueDateTime.ToString("dd/MM/yyy HH:mm"),
                [ContractPlaceholders.RentalPeriod] = contract.TotalDays.ToString(),
                [ContractPlaceholders.RentalPeriodDescription] = NumberToWords.Convert(contract.TotalDays),
                [ContractPlaceholders.RentalPeriodInDays] = contract.TotalDays == 1 ? "dia" : "dias",
                [ContractPlaceholders.ContractedMileage] = contract.MileageContracted.ToString(),
                [ContractPlaceholders.IncludedKm] = contract.MileageContracted.ToString(),
                [ContractPlaceholders.TotalPrice] = contract.TotalAmount.ToString("C", Culture),
                [ContractPlaceholders.TotalPriceInWords] = FormatCurrencyInWords(contract.TotalAmount),
                [ContractPlaceholders.DailyPrice] = FormatCurrency(contract.SnapshotPriceDailyRate),
                [ContractPlaceholders.DailyPriceInWords] = FormatCurrencyInWords(contract.SnapshotPriceDailyRate),
                [ContractPlaceholders.ExcessKmPrice] = FormatCurrency(contract.SnapshotPricePerExtraMileage),
                [ContractPlaceholders.ExcessKmPriceInWords] = FormatCurrencyInWords(contract.SnapshotPricePerExtraMileage),
                [ContractPlaceholders.RentalMode] = contract.RentalType,

                [ContractPlaceholders.ContractCity] = contract.Vehicle.Company.Address.City,
                [ContractPlaceholders.ContractState] = contract.Vehicle.Company.Address.State,
                [ContractPlaceholders.ContractDate] = contract.PickupDateTime.ToString("dd  MMMM yyyy"),
                [ContractPlaceholders.ContractNumber] = contract.Id.ToString(),
                [ContractPlaceholders.ContractStatus] = contract.Status.Label,
            };

            var result = templateContent;
            foreach (var (placeholder, value) in values)
                result = result.Replace(placeholder, value);

            return result;
        }

        private static string FormatMileage(long km)
        {
            return $"{km.ToString("N0", Culture)} ({NumberToWords.Convert(km)}) km";
        }

        private static string FormatCurrency(decimal amount)
        {
            return $"{amount.ToString("C", Culture)} ({FormatCurrencyInWords(amount)})";
        }

        private static string FormatCurrencyInWords(decimal amount)
        {
            var reais = (long)amount;
            var centavos = (int)Math.Round((amount - reais) * 100);

            var reaisWord = reais == 1 ? "real" : "reais";
            var extenso = NumberToWords.Convert(reais) + " " + reaisWord;

            if (centavos > 0)
                extenso += $" e {NumberToWords.Convert(centavos)} centavos";

            return extenso;
        }
    }
}