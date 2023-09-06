using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace CurrencyTranscriberClient.ValidationRules
{
    public class CurrencyTextboxValidationRule : ValidationRule
    {
        private static readonly Regex _numericRegex = new Regex("[^0-9.-]+");
        private static readonly Regex _decimalRegex = new Regex("^[-+]?\\d{1,3}(\\s\\d{3})*(,\\d+)?$");

        public CurrencyTextboxValidationRule() { }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!_numericRegex.IsMatch(value.ToString()))
                return new ValidationResult(false, "Input can only contain numbers");
            if (!_decimalRegex.IsMatch(value.ToString()))
                return new ValidationResult(false, "Input is not in correct format");

            return ValidationResult.ValidResult;
        }
    }
}
