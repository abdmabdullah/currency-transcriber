using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CurrencyTranscriberClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly HttpClient httpClient;

        public MainWindow(HttpClient httpClient)
        {
            InitializeComponent();
            this.httpClient = httpClient;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            HttpResponseMessage response = httpClient.GetAsync($"currency/GetTranscribedCurrency?number={currencyTextBox.Text}").Result;
            if (response.IsSuccessStatusCode)
            {
                var words = response.Content.ReadFromJsonAsync<string>().Result;
            }
        }
    }
}
