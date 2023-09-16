using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace CurrencyTranscriberClient.Models
{
    public class TranscribedCurrency : INotifyPropertyChanged, IDataErrorInfo
    {
        private static readonly Regex _numericRegex = new Regex("[^0-9.-]+");
        private static readonly Regex _decimalRegex = new Regex("^[-+]?\\d{1,3}(\\s\\d{3})*(,\\d+)?$");

        public string this[string property]
        {
            get
            {
                string? result = null;
                switch(property)
                {
                    case "CurrencyInWords":
                        if(CurrencyInWords is null)
                        {
                            result = "Amount cannot be empty";
                        }
                        else if (!_numericRegex.IsMatch(CurrencyInWords.ToString()))
                        {
                            result = "Input can only contain numbers";
                        }
                        else if (!_decimalRegex.IsMatch(CurrencyInWords.ToString()))
                        {
                            result = "Input is not in a correct format";
                        }
                        break;
                }
                return result;
            }
        }

        private string? _currencyInWords;
        public string? CurrencyInWords 
        {
            get
            {
                return _currencyInWords;
            }
            set
            {
                if(_currencyInWords != value)
                {
                    _currencyInWords = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Error => throw new NotImplementedException();

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
