using FleetManager.Communication.Response.ToContract;
using FleetManager.Domain.Entities;
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
                
                [ContractPlaceholders.TenantName] = contract.Tenant.Name,
                [ContractPlaceholders.VehiclePlate] = contract.Vehicle.LicensePlate,
                [ContractPlaceholders.PrazoLocacao] = FormatDays(contract.TotalDays),
                [ContractPlaceholders.DataEntregaPrevista] = contract.ReturnDueDateTime.ToString("dd/MM/yyyy 'às' HH:mm'h'", Culture),
                [ContractPlaceholders.QuilometragemContratada] = FormatMileage(contract.MileageContracted),
                [ContractPlaceholders.ValorTotal] = FormatCurrency(contract.TotalAmount),
            };

            var result = templateContent;
            foreach (var (placeholder, value) in values)
                result = result.Replace(placeholder, value);

            return result;
        }

        private static string FormatDays(int totalDays)
        {
            var word = totalDays == 1 ? "dia" : "dias";
            return $"{totalDays} ({NumberToWords.Convert(totalDays)}) {word}";
        }

        private static string FormatMileage(long km)
        {
            return $"{km.ToString("N0", Culture)} ({NumberToWords.Convert(km)}) km";
        }

        private static string FormatCurrency(decimal amount)
        {
            var reais = (long)amount;
            var centavos = (int)Math.Round((amount - reais) * 100);

            var reaisWord = reais == 1 ? "real" : "reais";
            var extenso = NumberToWords.Convert(reais) + " " + reaisWord;

            if (centavos > 0)
                extenso += $" e {NumberToWords.Convert(centavos)} centavos";

            return $"{amount.ToString("C", Culture)} ({extenso})";
        }
    }
}