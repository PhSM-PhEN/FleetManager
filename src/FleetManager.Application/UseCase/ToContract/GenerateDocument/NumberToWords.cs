namespace FleetManager.Application.UseCase.ToContract.GenerateDocument
{
    public static class NumberToWords
    {
        private static readonly string[] Units =
            ["", "um", "dois", "três", "quatro", "cinco", "seis", "sete", "oito", "nove"];
        private static readonly string[] Teens =
            ["dez", "onze", "doze", "treze", "quatorze", "quinze", "dezesseis", "dezessete", "dezoito", "dezenove"];
        private static readonly string[] Tens =
            ["", "", "vinte", "trinta", "quarenta", "cinquenta", "sessenta", "setenta", "oitenta", "noventa"];
        private static readonly string[] Hundreds =
            ["", "cento", "duzentos", "trezentos", "quatrocentos", "quinhentos", "seiscentos", "setecentos", "oitocentos", "novecentos"];

        public static string Convert(long number)
        {
            if (number == 0) return "zero";
            if (number < 0) return "menos " + Convert(-number);

            if (number >= 1_000_000)
            {
                var millions = number / 1_000_000;
                var rest = number % 1_000_000;
                var word = millions == 1 ? "um milhão" : $"{Convert(millions)} milhões";
                return rest == 0 ? word : $"{word}{Connector(rest)}{Convert(rest)}";
            }

            if (number >= 1000)
            {
                var thousands = number / 1000;
                var rest = number % 1000;
                var word = thousands == 1 ? "mil" : $"{Convert(thousands)} mil";
                return rest == 0 ? word : $"{word}{Connector(rest)}{Convert(rest)}";
            }

            if (number == 100) return "cem";

            if (number >= 100)
            {
                var hundred = Hundreds[number / 100];
                var rest = number % 100;
                return rest == 0 ? hundred : $"{hundred} e {Convert(rest)}";
            }

            if (number >= 20)
            {
                var ten = Tens[number / 10];
                var rest = number % 10;
                return rest == 0 ? ten : $"{ten} e {Units[rest]}";
            }

            if (number >= 10) return Teens[number - 10];

            return Units[number];
        }

        // Regra de "e" em números por extenso pt-BR: usa "e" antes do último grupo quando ele é
        // menor que 100 (ex.: "mil e um") ou é uma centena redonda (ex.: "mil e quinhentos").
        // Quando o grupo já tem dezena/unidade além da centena (ex.: 550 -> "quinhentos e cinquenta"),
        // o "e" já fica embutido dentro do próprio Convert(rest), então aqui não se repete.
        private static string Connector(long rest) => (rest < 100 || rest % 100 == 0) ? " e " : " ";
    }
}