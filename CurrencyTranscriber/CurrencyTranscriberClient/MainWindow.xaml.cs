using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Windows;

namespace CurrencyTranscriberClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        private readonly HttpClient httpClient;

        public MainWindow(HttpClient httpClient)
        {
            DataContext = this;
            InitializeComponent();
            this.httpClient = httpClient;
        }

        private string _currencyInWords;
        public string CurrencyInWords
        {
            get
            {
                return _currencyInWords;
            }
            set
            {
                if (_currencyInWords != value)
                {
                    _currencyInWords = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string propertyName = null) 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
    }
}
