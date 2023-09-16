using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;

namespace CurrencyTranscriberClient
{
    public partial class MainWindow : INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly HttpClient httpClient;

        public MainWindow(HttpClient httpClient)
        {
            DataContext = this;
            InitializeComponent();
            this.httpClient = httpClient;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _dollarAmount;

        public string DollarAmount
        {
            get { return _dollarAmount; }
            set { if (_dollarAmount != value) { _dollarAmount = value; OnPropertyChanged(); } }
        }

        private string _currencyInWords;

        public string CurrencyInWords
        {
            get { return _currencyInWords; }
            set { if (_currencyInWords != value) { _currencyInWords = value; OnPropertyChanged(); } }
        }

      
        public string Error => throw new System.NotImplementedException();

        

        public string this[string property]
        {
            get { if (property == "DollarAmount") return ValidateDollarAmount(); return null; }
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"currency/GetTranscribedCurrency?number={currencyTextBox.Text}");

            if (response.IsSuccessStatusCode)
            {
                string? words = await response.Content.ReadAsStringAsync();

                if(!string.IsNullOrEmpty(words)) 
                {
                    CurrencyInWords = words;
                }
            }
        }

        private string? ValidateDollarAmount()
        {
            Regex _numericRegex = new Regex("[^0-9.-]+");
            Regex _decimalRegex = new Regex("^[-+]?\\d{1,3}(\\s\\d{3})*(,\\d+)?$");

            if (DollarAmount is null)
                return "Amount cannot be empty";

            if (!_numericRegex.IsMatch(DollarAmount) && !_decimalRegex.IsMatch(DollarAmount))

                return "Input is not a valid format";

            string[] splitDollarAndCents = DollarAmount.Split(',');

            if (splitDollarAndCents.Length > 1 && splitDollarAndCents[1].Length > 2)
                return "Cents cannot be greater than 99";

            return default;
        }
    }
}
