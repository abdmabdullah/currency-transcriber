using System.Reflection.Metadata.Ecma335;

namespace CurrencyTranscriberApi.Helpers
{
    public static class CurrencyHelper
    {
        public static readonly char DECIMAL_SEPARATOR = ',';

        private static readonly string[] units =
        {
            "zero", "one", "two",
            "three", "four", "five",
            "six", "seven", "eight",
            "nine", "ten", "eleven",
            "twelve", "thirteen", "fourteen",
            "fifteen", "sixteen", "seventeen",
            "eighteen", "nineteen"
        };

        private static readonly string[] tens =
        {
            "zero", "ten", "twenty",
            "thirty", "forty", "fifty",
            "sixty", "seventy", "eighty",
            "ninety"
        };

        private static readonly string[] teens =
            { "", "eleven", "twelve",
            "thirteen", "fourteen", "fifteen",
            "sixteen", "seventeen", "eighteen",
            "nineteen" };

        public static string GetWords(int amount)
        {
            if (amount == 0)
                return "";
            else if (amount < 10)
                return units[amount];
            else if (amount < 20)
                return teens[amount - 10];
            else if (amount < 100)
                return tens[amount / 10] + ((amount % 10 > 0) ? "-" + units[amount % 10] : "");
            else if (amount < 1000)
                return units[amount / 100] + " hundred" + ((amount % 100 > 0) ? " " + GetWords(amount % 100) : "");
            else if (amount < 1000000)
                return GetWords(amount / 1000) + " thousand" + ((amount % 1000 > 0) ? " " + GetWords(amount % 1000) : "");
            else
                return GetWords(amount / 1000000) + " million" + ((amount % 1000000 > 0) ? " " + GetWords(amount % 1000000) : "");
        }

        public static string GetFormattedCurrencyInWords(string dollarWords, string centWords = "")
        {
            if(!string.IsNullOrWhiteSpace(centWords))
                return $"{dollarWords} dollars and {centWords} {GetCentKeyword(centWords)}";
            else
                return $"{dollarWords} dollars";
        }

        private static string GetCentKeyword(string centWords)
        {
            return centWords == "one" ? "cent" : "cents";
        }
    }
}
